using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(PlayerEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.PlayerEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.RoomEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static partial class PlayerEventTypeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.PlayerEventTypeComponent self, ET.Server.RoomEventTypeComponent roomEventType)
        {
            self.RoomEvent = roomEventType;
            roomEventType.PlayerEventTypeComponents.Add(self);
            RoomPlayer roomPlayer = self.GetParent<RoomPlayer>();
            CardGameComponent_Player myCards = roomPlayer.GetComponent<CardGameComponent_Player>();
            //挂载基础事件
            //Log.Warning("挂载基础事件:回合开始时抽牌");
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(roomPlayer), 
                GameEventFactory.GetHandCardsFromGroup(roomEventType, roomPlayer, 1));
            //Log.Warning("挂载基础事件:回合开始时得到新费用");
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(roomPlayer), 
                GameEventFactory.GetCostTotal(roomEventType, roomPlayer, 1));
            //Log.Warning("挂载基础事件:回合开始时费用恢复");
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(roomPlayer), 
                GameEventFactory.ResetCost(roomEventType, roomPlayer));
            //Log.Warning("挂载基础事件:回合开始时获得资质");
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(roomPlayer), 
                GameEventFactory.GetBaseColor(roomEventType, roomPlayer));
            //Log.Warning("挂载基础事件:回合开始时，所有单位和干员攻击次数清空");
            self.WaitGameEventTypes.Add(TriggerEventFactory.MyTurnStart(roomPlayer), 
                GameEventFactory.AllAttackCountClear(roomEventType, self, myCards));
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.PlayerEventTypeComponent self)
        {
            self.GetParent<Room>().GetComponent<RoomEventTypeComponent>().PlayerEventTypeComponents.Remove(self);
        }

        public static async ETTask<bool> SendTriggeerEvent(this PlayerEventTypeComponent self, GameEvent eventType, EventInfo eventInfo)
        {
            //通知所有监听器
            foreach (var kv in self.WaitGameEventTypes)
            {
                //Log.Warning($"服务器检测事件:{kv.Value.GameEventType}是否触发");
                if (eventType.IsDispose) return true;
                if (kv.Key.Triggeer.Invoke(eventType)) {
                    //Log.Warning($"服务器事件:{kv.Value.GameEventType}被{eventType.GameEventType}触发");
                    await kv.Value.ToDo(eventType, eventInfo);
                }
            }

            return false;
        }
    }
}