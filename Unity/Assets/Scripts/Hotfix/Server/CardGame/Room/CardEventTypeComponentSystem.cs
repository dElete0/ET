using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static partial class CardEventTypeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.CardEventTypeComponent self, ET.Server.RoomEventTypeComponent args2)
        {
            self.RoomEvent = args2;
            self.RoomEvent.CardEventTypeComponents.Add(self);
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.CardEventTypeComponent self)
        {
            self.RoomEvent.CardEventTypeComponents.Remove(self);
        }

        public static bool SendTriggeerEvent(this ET.Server.CardEventTypeComponent self, GameEvent eventType)
        {
            //通知所有监听器
            foreach (var kv in self.AllGameEventTypes)
            {
                if (SendTriggeerEventHelper(kv, eventType)) {
                    return true;
                }
            }
            //单位监听器
            if (self.UnitGameEventTypes.Count > 0 &&
                self.GetParent<Room>().GetComponent<CardGameComponent_Cards>().IsUnit(self.GetParent<RoomCard>())) {
                foreach (var kv in self.UnitGameEventTypes)
                {
                    if (SendTriggeerEventHelper(kv, eventType)) {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool SendTriggeerEventHelper(KeyValuePair<TriggerEvent, GameEvent> kv, GameEvent eventType) {
            if (eventType.IsDispose) {
                //被什么奇怪的效果改了，重新遍历
                return true;
            }
            if (kv.Key.Triggeer.Invoke(eventType))
            {
                kv.Value.ToDo(eventType);
            }
            return false;
        }

        public static void PowerToDo(this Power_Struct power, RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            switch (power.PowerType)
            {
                //抽卡效果
                case Power_Type.GetHandCardFromGroup:
                    room.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(room, player, power.Count1));
                    break;
                case Power_Type.DamageHint:
                    room.BroadAndSettleEvent(GameEventFactory.Damage(room, card, target, power.Count1));
                    break;
            }
        }
    }
}