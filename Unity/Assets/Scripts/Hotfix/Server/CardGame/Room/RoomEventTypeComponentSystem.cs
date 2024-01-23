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

        //仅供C2Room等调用
        public static async ETTask SettleEventWithLock(this ET.Server.RoomEventTypeComponent self, GameEvent eventType, EventInfo eventInfo) {
            using (await self.Root().GetComponent<CoroutineLockComponent>()
                           .Wait(CoroutineLockType.LoginAccount, self.Id.ToString().GetLongHashCode())) {
                await self.BroadEvent(eventType, eventInfo);
                Log.Warning("锁内逻辑执行完毕");
            }
        }

        /// <summary>
        /// 只能由 SettleEventWithLock 或 ToDo的静态方法调用
        /// </summary>
        /// <param name="self"></param>
        /// <param name="eventType"></param>
        /// <param name="eventInfo"></param>
        public static async ETTask BroadEvent(this ET.Server.RoomEventTypeComponent self, GameEvent eventType, EventInfo eventInfo) {
            eventInfo.Count++;
            Log.Warning($"服务器检测:{eventType.ToStr()}的相关监听器");
            if (self.Count < Msg_Room.EventToDoCountMax) {
                self.Count++;
                //如果触发了其中的事件，就直接ToDo事件,如果导致eventType失效，就不执行了
                foreach (var eventTypeComponent in self.CardEventTypeComponents) {
                    if (await eventTypeComponent.IsEventBeOverByTriggeerEvent(eventType, eventInfo)) {
                        eventInfo.Count--;
                        return;
                    }
                }
            } else {
                Log.Warning($"{eventType.ToStr()}已触发最大逻辑执行次数");
                eventInfo.Count--;
            }
            
            // CardEvent 优先级高于PlayerEvent上的基础事件
            // PlayerEvent只存放基础事件及处理，不得修改当前事件
            foreach (var eventTypeComponent in self.PlayerEventTypeComponents) {
                if (await eventTypeComponent.SendTriggeerEvent(eventType, eventInfo)) {
                    eventInfo.Count--;
                    return;
                }
            }
            
            Log.Warning($"服务器处理开始:{eventType.ToStr()}");
            //事件执行
            if (eventType.ToDo != null) {
                await eventType.ToDo.Invoke(GameEvent.Instance, eventInfo);
            }

            Log.Warning($"服务器处理完成:{eventType.ToStr()}");
            eventInfo.Count--;
            if (eventInfo.Count < 1) {
                if (eventInfo.DeadList.Count > 0) {
                    Log.Warning("其他事件都执行了，执行死亡标记等事件");
                    await self.BroadEvent(GameEventFactory.Dead(self, eventInfo.DeadList), eventInfo);
                }

                if (eventInfo.RemoveList.Count > 0) {
                    await self.BroadEvent(GameEventFactory.Remove(self, eventInfo.RemoveList), eventInfo);
                }
                
                //继续执行新的逻辑
                if (eventInfo.PowerStructs.Count > 0) {
                    Log.Warning("继续执行新的逻辑");
                    var power = eventInfo.PowerStructs[0];
                    eventInfo.PowerStructs.RemoveAt(0);
                    await power.Item1.PowerToDo(self, eventInfo, power.Item2, power.Item3, power.Item4, power.Item5);
                    Log.Warning(eventInfo.PowerStructs.Count); 
                } else {
                    Log.Warning("没有新的逻辑需要执行");
                }
            }
        }

        private static string ToStr(this GameEvent gameEvent) {
            return gameEvent.GameEventType.ToString();
        }
    }
}