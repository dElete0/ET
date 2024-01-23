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

        public static async ETTask<bool> IsEventBeOverByTriggeerEvent(this ET.Server.CardEventTypeComponent self, GameEvent eventType, EventInfo eventInfo)
        {
            //通知任意位置触发的监听器
            foreach (var kv in self.AllGameEventTypes)
            {
                if (await SendTriggeerEventHelper(kv, eventType, eventInfo)) {
                    return true;
                }
            }
            //场上角色监听器
            if (self.UnitGameEventTypes.Count > 0 && 
                (self.GetParent<RoomCard>().CardType == CardType.Agent ||
                    self.GetParent<RoomCard>().CardType == CardType.Hero ||
                    self.GetParent<Room>().GetComponent<CardGameComponent_Cards>().IsUnit(self.GetParent<RoomCard>()))) {
                foreach (var kv in self.UnitGameEventTypes) {
                    if (await SendTriggeerEventHelper(kv, eventType, eventInfo)) {
                        return true;
                    }
                }
            }
            //手牌监听器
            if (self.HanCardEventTypes.Count > 0 && 
                (self.GetParent<Room>().GetComponent<CardGameComponent_Cards>().IsHandCards(self.GetParent<RoomCard>()))) {
                foreach (var kv in self.HanCardEventTypes) {
                    if (await SendTriggeerEventHelper(kv, eventType, eventInfo)) {
                        return true;
                    }
                }
            }

            return false;
        }

        private static async ETTask<bool> SendTriggeerEventHelper(KeyValuePair<TriggerEvent, GameEvent> kv, GameEvent eventType, EventInfo eventInfo) {
            if (eventType.IsDispose) {
                //被什么奇怪的效果改了，重新遍历
                return true;
            }
            if (kv.Key.Triggeer.Invoke(eventType))
            {
                await kv.Value.ToDo(eventType, eventInfo);
            }
            return false;
        }

        public static async ETTask PowerToDo(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, List<RoomCard> targets, RoomPlayer player)
        {
            switch (power.PowerType) {
                case Power_Type.GetHandCardFromGroup:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageHurt:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.Damage(roomEventTypeComponent, actor, target, power.Count1), eventInfo);
                    break;
                case Power_Type.Desecrate:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.Desecrate(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.DamageAllUnit:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.DamageAllUnit(roomEventTypeComponent, actor, player, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageAllActor:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.DamageAllActor(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.CallTargetUnitByBaseId:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnitByBaseId(roomEventTypeComponent, player, target, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.EnemyCallTargetUnitByBaseId:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnitByBaseId(roomEventTypeComponent, actor.GetOwner().GetEnemy(), target, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.SilentTarget:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.SilentTarget(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.AttributeAura:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AttributeAuraEffect(roomEventTypeComponent, actor, power), eventInfo);
                    break;
                case Power_Type.CallRedDragon:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.CallRedDragon(roomEventTypeComponent, player, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.KillTargetUnit:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.KillTargetUnit(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.KillAllUnit:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.KillAllUnit(roomEventTypeComponent, actor), eventInfo);
                    break;
                case Power_Type.FindAndCloneCard:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.FindAndCloneCard(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.GetQualifications:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetQualifications(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.GetArmor:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetArmor(roomEventTypeComponent, actor, player, power.Count1), eventInfo);
                    break;
                case Power_Type.TargetGetPower:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TargetGetPower(roomEventTypeComponent, actor, target, (Power_Type)power.Count1), eventInfo);
                    break;
                case Power_Type.RemoveTargetUnit:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.RemoveTargetUnit(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.TreatTarget:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TreatTarget(roomEventTypeComponent, actor, target, power.Count1), eventInfo);
                    break;
                case Power_Type.TreatMyHero:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TreatTarget(roomEventTypeComponent, actor, actor.GetOwner().GetHero(), power.Count1), eventInfo);
                    break;
                case Power_Type.AddCardToGroupByBaseIdShow:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AddCardToGroupByBaseIdShow(roomEventTypeComponent, actor, power.Count1, power.Count2, power.Count3), eventInfo);
                    break;
                case Power_Type.AddCardToGroupByBaseIdHide:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AddCardToGroupByBaseIdHide(roomEventTypeComponent, actor, power.Count1, power.Count2, power.Count3), eventInfo);
                    break;
                case Power_Type.SwapArmor:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.SwapArmor(roomEventTypeComponent, actor), eventInfo);
                    break;
                case Power_Type.TargetGetAttribute:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TargetGetAttribute(roomEventTypeComponent, actor, target, power.Count1), eventInfo);
                    break;
                case Power_Type.GoldenShip:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GoldenShip(roomEventTypeComponent, actor), eventInfo);
                    break;
                case Power_Type.PowerToUseBaseCard:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.PowerToUseBaseCard(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageEnemyHero:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.DamageEnemyHero(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.DamageMyHero:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.DamageMyHero(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.MyHeroGetAttackThisTurn:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.MyHeroGetAttackThisTurn(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.UnitsGetAttribute:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.UnitsGetAttribute(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.UnitsInGroupGetAttribute:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.UnitsInGroupGetAttribute(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.UnitsInGroupLoseAttributeAddDamageEnemyHero:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.UnitsInGroupLoseAttributeAddDamageEnemyHero(roomEventTypeComponent, actor, power.Count1, power.Count2), eventInfo);
                    break;
                case Power_Type.TargetBackToHandCards:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TargetBackToHandCards(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.GetHandCards:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetHandCards(roomEventTypeComponent, player, target, targets), eventInfo);
                    break;
                case Power_Type.EnemyGetHandCardsByBaseId:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetHandCardsByBaseIds(roomEventTypeComponent, player.GetEnemy(), power.Count1, power.Count2, power.Count3), eventInfo);
                    break;
                case Power_Type.TargetBackToGroup:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.TargetBackToGroup(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.AddCardToGroupShow:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AddTargetCardToGroupShow(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.Erosion:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.Erosion(roomEventTypeComponent, actor, power.Count1), eventInfo);
                    break;
                case Power_Type.MyHeroGetTargetPowerThisTurn:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.MyHeroGetTargetPowerThisTurn(roomEventTypeComponent, actor, (Power_Type)power.Count1), eventInfo);
                    break;
                case Power_Type.GetHandCardsByBaseIds:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.GetHandCardsByBaseIds(roomEventTypeComponent, actor.GetOwner(), power.Count1, power.Count2, power.Count3), eventInfo);
                    break;
                case Power_Type.RemoveArmorAndDamageThisNum:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.RemoveArmorAndDamageThisNum(roomEventTypeComponent, actor), eventInfo);
                    break;
                case Power_Type.SendUnitToEnemy:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.SendUnitToEnemy(roomEventTypeComponent, actor, target), eventInfo);
                    break;
                case Power_Type.CallTargetUnit:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnit(roomEventTypeComponent, player, target, power.Count1), eventInfo);
                    break;
                case Power_Type.CallTargetUnitForAllByBaseId:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnitForAllByBaseId(roomEventTypeComponent, actor, power.Count1, power.Count2, power.Count3), eventInfo);
                    break;
            }
        }

        public static async ETTask PowerToLose(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, RoomPlayer player) {
            Log.Warning("光环失效");
            switch (power.PowerType) {
                case Power_Type.AttributeAura:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AttributeAuraUnEffect(roomEventTypeComponent, actor, power), eventInfo);
                    break;
            }
        }

        public static async ETTask AuraPowerToTarget(this Power_Struct power, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, RoomPlayer player) {
            switch (power.PowerType) {
                case Power_Type.AttributeAura:
                    await roomEventTypeComponent.BroadEvent(GameEventFactory.AttributeAuraEffectToTarget(roomEventTypeComponent, actor, power.TriggerEvent, power.Count1), eventInfo);
                    break;
            }
        }
    }
}