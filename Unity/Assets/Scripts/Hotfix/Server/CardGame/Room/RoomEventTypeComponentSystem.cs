using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(RoomEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    public static partial class RoomEventTypeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.RoomEventTypeComponent self)
        {

        }

        public static void CountClear(this ET.Server.RoomEventTypeComponent self) {
            self.Count = 0;
        }

        public static async ETTask BroadAndSettleEvent(this ET.Server.RoomEventTypeComponent self, GameEvent eventType, EventInfo eventInfo) {
            eventInfo.Count++;
            Log.Warning("服务器处理: " + eventType.ToStr());
            if (self.Count < Msg_Room.EventToDoCountMax) {
                self.Count++;
                //如果触发了其中的事件，就直接ToDo事件,如果导致eventType失效，就不执行了
                foreach (var eventTypeComponent in self.CardEventTypeComponents) {
                    if (eventTypeComponent.SendTriggeerEvent(eventType, eventInfo)) {
                        eventInfo.Count--;
                        return;
                    }
                }
            }
            
            // CardEvent 优先级高于PlayerEvent上的基础事件
            // PlayerEvent只存放基础事件及处理，不得修改当前事件
            foreach (var eventTypeComponent in self.PlayerEventTypeComponents) {
                if (eventTypeComponent.SendTriggeerEvent(eventType, eventInfo)) {
                    eventInfo.Count--;
                    return;
                }
            }
            
            //事件执行
            if (eventType.ToDo != null) {
                try {
                    eventType.ToDo.Invoke(null, eventInfo);
                } catch (Exception e) {
                    Log.Error(e.ToString());
                }
            }

            eventInfo.Count--;
            if (eventInfo.Count < 1) {
                //其他事件都执行了，执行死亡标记等事件
                if (eventInfo.DeadList.Count > 0) {
                    await self.BroadAndSettleEvent(GameEventFactory.Dead(self, eventInfo.DeadList), eventInfo);
                    eventInfo.DeadList.Clear();
                }
            
                //继续执行新的逻辑
                Log.Warning(eventInfo.PowerStructs.Count);
                if (eventInfo.PowerStructs.Count > 0) {
                    foreach (var power in eventInfo.PowerStructs) {
                        Log.Warning(eventInfo.PowerStructs.Count);
                        await power.Item1.PowerToDo(self, eventInfo, power.Item2, power.Item3, power.Item4);
                    }
                }
            }
        }

        private static string ToStr(this GameEvent gameEvent) {
            return gameEvent.GameEventType.ToString();
        }
    }
}