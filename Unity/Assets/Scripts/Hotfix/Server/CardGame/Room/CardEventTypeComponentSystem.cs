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
                kv.Value.ToDo(eventType, eventInfo);
            }
            return false;
        }

        public static async ETTask PowerToDo(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, RoomPlayer player)
        {
            switch (power.PowerType) {
                case Power_Type.GetHandCardFromGroup:
                    //抽卡效果
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageHurt:
                    //直伤
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, actor, target, power.Count1), eventInfo);
                    break;
                case Power_Type.Desecrate:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Desecrate(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageAllUnit:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.DamageAllUnit(roomEventTypeComponent, actor, player, power.Count1), eventInfo);
                    break;
                case Power_Type.CallTargetUnit:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.CallTargetUnit(roomEventTypeComponent, player, target, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.SilentTarget:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.SilentTarget(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.AttributeAura:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttributeAuraEffect(roomEventTypeComponent, actor, power), eventInfo);
                    break;
                case Power_Type.CallRedDragon:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.CallRedDragon(roomEventTypeComponent, player, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.KillTargetUnit:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.KillTargetUnit(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.KillAllUnit:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.KillAllUnit(roomEventTypeComponent, actor), eventInfo);
                    break;
                case Power_Type.FindAndCloneCard:
                    await roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.FindAndCloneCard(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
            }
        }

        public static void PowerToLose(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, RoomPlayer player) {
            switch (power.PowerType) {
                case Power_Type.AttributeAura:
                    roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttributeAuraUnEffect(roomEventTypeComponent, actor, power), eventInfo);
                    break;
            }
        }

        public static void AuraPowerToTarget(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, RoomPlayer player) {
            switch (power.PowerType) {
                case Power_Type.AttributeAura:
                    roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttributeAuraEffectToTarget(roomEventTypeComponent, actor, power.TriggerEvent, power.Count1), eventInfo);
                    break;
            }
        }
    }
}