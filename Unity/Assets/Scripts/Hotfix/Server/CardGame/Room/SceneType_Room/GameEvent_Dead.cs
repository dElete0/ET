using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_Dead
    {
        public static async ETTask ToDo_UnitsDead(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, List<RoomCard> cards)
        {
            //Todo Unit里移除这项
            Room room = roomEventTypeComponent.GetParent<Room>();
            List<long> cardIds = new List<long>();

            List<RoomCard> deadList = new List<RoomCard>(cards);
            eventInfo.DeadList.Clear();
            //移除光环等效果
            foreach (var card in deadList) {
                await roomEventTypeComponent.ToDo_AuraUnEffect(eventInfo, card);
            }
            //触发亡语等其他效果
            foreach (var card in deadList) {
                await roomEventTypeComponent.BroadEvent(GameEventFactory.DeadOver(roomEventTypeComponent, card), eventInfo);
            }

            //移除单位
            foreach (var card in deadList)
            {
                cardIds.Add(card.Id);
                Log.Warning($"单位死亡:{card.Id}");
                roomEventTypeComponent.CardEventTypeComponents.Remove(card.GetComponent<CardEventTypeComponent>());
                room.GetComponent<CardGameComponent_Cards>().RemoveCard(card);
            }
            Room2C_UnitDead unitDead = new Room2C_UnitDead() { CardIds = cardIds };
            RoomMessageHelper.BroadCast(room, unitDead);
        }
        
        public static async ETTask ToDo_RemoveUnits(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, List<(RoomCard, RemoveType)> cards)
        {
            //Todo Unit里移除这项
            Room room = roomEventTypeComponent.GetParent<Room>();
            List<long> cardIds = new List<long>();
            List<int> removeTypes = new List<int>();

            List<(RoomCard, RemoveType)> deadList = new List<(RoomCard, RemoveType)>(cards);
            eventInfo.DeadList.Clear();
            //移除光环等效果
            foreach (var card in deadList) {
                await roomEventTypeComponent.ToDo_AuraUnEffect(eventInfo, card.Item1);
            }

            //移除单位
            foreach (var card in deadList)
            {
                cardIds.Add(card.Item1.Id);
                removeTypes.Add((int)card.Item2);
                roomEventTypeComponent.CardEventTypeComponents.Remove(card.Item1.GetComponent<CardEventTypeComponent>());
            }
            Room2C_RemoveUnits message = new Room2C_RemoveUnits() { CardIds = cardIds, RemoveType = removeTypes};
            RoomMessageHelper.BroadCast(room, message);
        }

        public static async ETTask ToDo_Fall(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
        {
            foreach (var powerStruct in card.OtherPowers)
            {
                if (powerStruct.TriggerPowerType == TriggerPowerType.Dead)
                {
                    await powerStruct.PowerToDo(roomEventTypeComponent, eventInfo, card, null, null, card.GetOwner());
                }
            }
        }

        public static async ETTask ToDo_KillTargetUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target)
        {
            await ETTask.CompletedTask;
            eventInfo.DeadList.Add(target);
        }
        
        public static async ETTask ToDo_RemoveTargetUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target)
        {
            await ETTask.CompletedTask;
            eventInfo.RemoveList.Add((target, RemoveType.Nomal));
        }

        public static async ETTask ToDO_KillAllUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor)
        {
            await ETTask.CompletedTask;
            List<long> unitids = new List<long>(actor.GetOwner().GetComponent<CardGameComponent_Player>().Units);
            unitids.AddRange(actor.GetOwner().GetEnemy().GetComponent<CardGameComponent_Player>().Units);
            CardGameComponent_Cards cards = actor.GetParent<CardGameComponent_Cards>();
            foreach (var unitid in unitids) {
                eventInfo.DeadList.Add(cards.GetChild<RoomCard>(unitid));
            }
        }

        public static async ETTask ToDo_Erosion(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int num) {
            CardGameComponent_Player playerInfo = actor.GetOwner().GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards cards = actor.GetParent<CardGameComponent_Cards>();
            List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
            for (int i = 0; i < num; i++) {
                RoomCard card = cards.GetChild<RoomCard>(playerInfo.Groups[0]);
                playerInfo.Groups.RemoveAt(0);
                cardInfos.Add(card.RoomCard2MyHandCardInfo());
                cards.RemoveChild(card.Id);
            }

            if (cardInfos.Count > 0) {
                Room2C_BreakCards myMessage = new Room2C_BreakCards() { Cards = cardInfos, IsMy = true };
                Room2C_BreakCards enemyMessage = new Room2C_BreakCards() { Cards = cardInfos, IsMy = false };
                RoomMessageHelper.ServerSendMessageToClient(actor.GetOwner(), myMessage);
                RoomMessageHelper.BroadCastWithOutPlayer(actor.GetOwner().GetEnemy(), enemyMessage);
            }
            await ETTask.CompletedTask;
        }
    }
}