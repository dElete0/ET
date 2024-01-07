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

        public static void BroadAndSettleEvent(this ET.Server.RoomEventTypeComponent self, GameEvent eventType)
        {
            Log.Warning("处理: " + eventType.ToStr());
            if (self.Count < Msg_Room.EventToDoCountMax) {
                self.Count++;
                //如果触发了其中的事件，就直接ToDo事件,如果导致eventType失效，就不执行了
                foreach (var eventTypeComponent in self.CardEventTypeComponents) {
                    if (eventTypeComponent.SendTriggeerEvent(eventType)) {
                        return;
                    }
                }
            }
            
            // CardEvent 优先级高于PlayerEvent上的基础事件
            // PlayerEvent只存放基础事件及处理，不得修改当前事件
            foreach (var eventTypeComponent in self.PlayerEventTypeComponents) {
                if (eventTypeComponent.SendTriggeerEvent(eventType)) {
                    return;
                }
            }
            
            //事件执行
            if (eventType.ToDo != null) {
                try {
                    eventType.ToDo(null);
                } catch (Exception e) {
                    Log.Error(e.ToString());
                }
            }
        }

        private static string ToStr(this GameEvent gameEvent) {
            return gameEvent.GameEventType.ToString();
        }
    }
}