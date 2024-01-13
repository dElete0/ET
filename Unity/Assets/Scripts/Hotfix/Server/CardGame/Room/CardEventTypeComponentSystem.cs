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

        public static bool SendTriggeerEvent(this ET.Server.CardEventTypeComponent self, GameEvent eventType, EventInfo eventInfo)
        {
            //通知任意位置触发的监听器
            foreach (var kv in self.AllGameEventTypes)
            {
                if (SendTriggeerEventHelper(kv, eventType, eventInfo)) {
                    return true;
                }
            }
            //场上角色监听器
            if (self.UnitGameEventTypes.Count > 0 && 
                (self.GetParent<RoomCard>().CardType == CardType.Agent ||
                    self.GetParent<RoomCard>().CardType == CardType.Hero ||
                    self.GetParent<Room>().GetComponent<CardGameComponent_Cards>().IsUnit(self.GetParent<RoomCard>()))) {
                foreach (var kv in self.UnitGameEventTypes)
                {
                    if (SendTriggeerEventHelper(kv, eventType, eventInfo)) {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool SendTriggeerEventHelper(KeyValuePair<TriggerEvent, GameEvent> kv, GameEvent eventType, EventInfo eventInfo) {
            if (eventType.IsDispose) {
                //被什么奇怪的效果改了，重新遍历
                return true;
            }
            if (kv.Key.Triggeer.Invoke(eventType))
            {
                kv.Value.ToDo(eventInfo);
            }
            return false;
        }

        public static void PowerToDo(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo)
        {
            switch (power.PowerType) {
                case Power_Type.GetHandCardFromGroup:
                    //抽卡效果
                    roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, power.RoomPlayer1, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageHurt:
                    //直伤
                    roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, power.Card1, power.Card2, power.Count1), eventInfo);
                    break;
                case Power_Type.Desecrate:
                    roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Desecrate(roomEventTypeComponent, power.Card1, power.Count1), eventInfo);
                    break;
            }
        }
    }
}