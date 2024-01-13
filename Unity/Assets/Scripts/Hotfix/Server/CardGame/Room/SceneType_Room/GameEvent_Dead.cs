using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    public static class GameEvent_Dead
    {
        public static void ToDo_UnitDead(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, List<RoomCard> cards)
        {
            //Todo Unit里移除这项
            Room room = roomEventTypeComponent.GetParent<Room>();
            List<long> cardIds = new List<long>();
            foreach (var card in cards)
            {
                cardIds.Add(card.Id);
                Log.Warning($"单位死亡:{card.Id}");
                roomEventTypeComponent.CardEventTypeComponents.Remove(card.GetComponent<CardEventTypeComponent>());
                room.GetComponent<CardGameComponent_Cards>().RemoveCard(card);
            }
            eventInfo.DeadList.Clear();
            Room2C_UnitDead unitDead = new Room2C_UnitDead() { CardIds = cardIds };
            RoomMessageHelper.BroadCast(room, unitDead);

            foreach (var card in cards)
            {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.DeadOver(roomEventTypeComponent, card), eventInfo);
            }
        }

        public static void ToDo_Fall(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
        {
            foreach (var powerStruct in card.OtherPowers)
            {
                if (powerStruct.TriggerPowerType == TriggerPowerType.Dead)
                {
                    powerStruct.PowerToDo(roomEventTypeComponent, eventInfo);
                }
            }
        }

        public static void ToDo_LeaveFight(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
        {
            CardEventTypeComponent cardEventTypeComponent = card.GetComponent<CardEventTypeComponent>();
            cardEventTypeComponent.UnitGameEventTypes.Clear();
        }
    }
}