using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(PlayerEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.PlayerEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    public static partial class PlayerEventTypeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.PlayerEventTypeComponent self, ET.Server.RoomEventTypeComponent args2)
        {
            self.RoomEvent = args2;
            args2.PlayerEventTypeComponents.Add(self);
            //挂载基础事件

            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(self.GetParent<RoomPlayer>()), new GameEvent(GameEventType.GetHandCard)
            {
                ToDo = (gameEvent) =>
                {
                    //Log.Warning("回合开始时抽卡");
                    args2.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(args2, self.GetParent<RoomPlayer>(), 1));
                },
            });
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(self.GetParent<RoomPlayer>()), new GameEvent(GameEventType.GetCostTotal)
            {
                ToDo = (gameEvent) =>
                {
                    //Log.Warning("回合开始时增加费用上限");
                    args2.BroadAndSettleEvent(GameEventFactory.GetCostTotal(args2, self.GetParent<RoomPlayer>(), 1));
                },
            });
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(self.GetParent<RoomPlayer>()), new GameEvent(GameEventType.ResetCost)
            {
                ToDo = (gameEvent) =>
                {
                    //Log.Warning("回合开始时费用复原");
                    args2.BroadAndSettleEvent(GameEventFactory.ResetCost(args2, self.GetParent<RoomPlayer>()));
                },
            });
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(self.GetParent<RoomPlayer>()), new GameEvent(GameEventType.GetBaseColor) {
                ToDo = (gameEvent) => {
                    args2.BroadAndSettleEvent(GameEventFactory.GetBaseColor(args2, self.GetParent<RoomPlayer>()));
                }
            });

        }
        [EntitySystem]
        private static void Destroy(this ET.Server.PlayerEventTypeComponent self)
        {
            self.GetParent<Room>().GetComponent<RoomEventTypeComponent>().PlayerEventTypeComponents.Remove(self);
        }

        public static bool SendTriggeerEvent(this PlayerEventTypeComponent self, GameEvent eventType)
        {
            //通知所有监听器
            foreach (var kv in self.WaitGameEventTypes)
            {
                if (eventType.IsDispose) return true;
                if (kv.Key.Triggeer.Invoke(eventType))
                {
                    kv.Value.ToDo(eventType);
                }
            }

            return false;
        }
    }
}