using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_Dead
    {
        public static void ToDo_UnitDead(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, List<RoomCard> cards)
        {
            //Todo Unit里移除这项
            Room room = roomEventTypeComponent.GetParent<Room>();
            List<long> cardIds = new List<long>();

            //触发亡语等其他效果
            foreach (var card in cards)
            {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.DeadOver(roomEventTypeComponent, card), eventInfo);
            }

            //移除单位
            foreach (var card in cards)
            {
                cardIds.Add(card.Id);
                Log.Warning($"单位死亡:{card.Id}");
                roomEventTypeComponent.CardEventTypeComponents.Remove(card.GetComponent<CardEventTypeComponent>());
                room.GetComponent<CardGameComponent_Cards>().RemoveCard(card);
            }
            Room2C_UnitDead unitDead = new Room2C_UnitDead() { CardIds = cardIds };
            RoomMessageHelper.BroadCast(room, unitDead);
        }

        public static void ToDo_Fall(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
        {
            foreach (var powerStruct in card.OtherPowers)
            {
                if (powerStruct.TriggerPowerType == TriggerPowerType.Dead)
                {
                    powerStruct.PowerToDo(roomEventTypeComponent, eventInfo, card, null, card.GetOwner());
                }
            }
        }

        public static void ToDo_LeaveFight(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
        {
            CardEventTypeComponent cardEventTypeComponent = card.GetComponent<CardEventTypeComponent>();
            cardEventTypeComponent.UnitGameEventTypes.Clear();
        }

        public static void ToDo_KillTargetUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target)
        {
            eventInfo.DeadList.Add(target);
        }

        public static void ToDO_KillAllUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor)
        {
            List<long> unitids = new List<long>(actor.GetOwner().GetComponent<CardGameComponent_Player>().Units);
            unitids.AddRange(actor.GetOwner().GetEnemy().GetComponent<CardGameComponent_Player>().Units);
            CardGameComponent_Cards cards = actor.GetParent<CardGameComponent_Cards>();
            List<RoomCard> units = new List<RoomCard>();
            foreach (var unitid in unitids) {
                units.Add(cards.GetChild<RoomCard>(unitid));
            }

            Room2C_UnitDead message = new Room2C_UnitDead() { CardIds = unitids };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
        }
    }
}