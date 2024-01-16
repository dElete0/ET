using System.Collections.Generic;
using NLog.Targets;

namespace ET.Server;
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
[FriendOfAttribute(typeof(ET.RoomCard))]
[FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
public static class GameEvent_UnitArrange
{
    public static void ToDo_UnitArrange(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player)
    {
        List<Power_Struct> powerStructs = card.GetArranges();
        foreach (var power in powerStructs)
        {
            power.PowerToDo(roomEventTypeComponent, eventInfo, card, target, player);
        }
    }

    public static void ToDo_AuraEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetAura();
        foreach (var power in powerStructs)
        {
            power.PowerToDo(roomEventTypeComponent, eventInfo, card, null, player);
        }

        CardEventTypeComponent cardEventTypeComponent = card.GetComponent<CardEventTypeComponent>();
        
        {
            //死亡或沉默，光环失效
            TriggerEvent triggerEvent = new TriggerEvent((gameEvent) => {
                if (gameEvent.GameEventType == GameEventType.DeadOver &&
                    gameEvent.Actor == card.Id)
                {
                    return true;
                }
                return false;
            });
            cardEventTypeComponent.UnitGameEventTypes.Add(triggerEvent, GameEventFactory.AuraUnEffect(roomEventTypeComponent, card));
        }

        {
            //新单位上场，光环生效
            TriggerEvent triggerEvent = new TriggerEvent((gameEvent) => {
                if (gameEvent.GameEventType == GameEventType.CallUnitOver &&
                    gameEvent.Actor != card.Id) {
                    return true;
                }

                return false;
            });
            cardEventTypeComponent.UnitGameEventTypes.Add(triggerEvent, GameEventFactory.AuraEffectToTarget(roomEventTypeComponent, card));
        }
    }

    public static void ToDo_AuraUnEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card)
    {
        List<Power_Struct> powerStructs = card.GetAura();
        foreach (var power in powerStructs)
        {
            power.PowerToLose(roomEventTypeComponent, eventInfo, card, null, null);
        }
    }

    public static void ToDo_AuraEffectToTarget(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, GameEvent trigger) {
        List<Power_Struct> powerStructs = card.GetAura();
        foreach (var power in powerStructs) {
            Power_Struct powerStruct = new Power_Struct();
            powerStruct.PowerType = power.PowerType;
            powerStruct.TriggerPowerType = power.TriggerPowerType;
            powerStruct.Count1 = power.Count1;
            powerStruct.Count2 = power.Count2;
            powerStruct.Count3 = power.Count3;
            powerStruct.TriggerEvent = trigger;
            powerStruct.AuraPowerToTarget(roomEventTypeComponent, eventInfo, card, null, null);
        }
    }

    public static void ToDo_AttributeAuraEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, Power_Struct power)
    {
        RoomPlayer player = card.GetOwner();
        CardGameComponent_Cards cardGameComponentCards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
        CardGameComponent_Player cards = player.GetComponent<CardGameComponent_Player>();
        List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
        foreach (var unitId in cards.Units)
        {
            RoomCard unit = cardGameComponentCards.GetChild<RoomCard>(unitId);
            unit.Attack += power.Count1;
            unit.HP += power.Count1;
            unit.HPMax += power.Count1;
            unit.AuraOnThisPowers.Add(power);
            cardInfos.Add(unit.RoomCard2AgentInfo());
        }

        Room2C_FlashMyUnit myMessage = new Room2C_FlashMyUnit() { Units = cardInfos };
        RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
        Room2C_FlashEnemyUnit enemyMessage = new Room2C_FlashEnemyUnit() { Units = cardInfos };
        RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
    }

    public static void ToDo_AttributeAuraUnEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, Power_Struct power) {
        RoomPlayer player = actor.GetOwner();
        CardGameComponent_Cards cardGameComponentCards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
        CardGameComponent_Player cards = player.GetComponent<CardGameComponent_Player>();
        List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
        foreach (var unitId in cards.Units)
        {
            RoomCard unit = cardGameComponentCards.GetChild<RoomCard>(unitId);
            if (unit.AuraOnThisPowers.Contains(power)) {
                Log.Warning($"单位{unitId}上的属性光环失效");
                unit.Attack -= power.Count1;
                unit.HP -= power.Count1;
                unit.HPMax -= power.Count1;
                if (unit.HP < 1)
                    unit.HP = 1;
                if (unit.HPMax < 1)
                    unit.HPMax = 1;
                cardInfos.Add(unit.RoomCard2AgentInfo());
            }
        }

        Room2C_FlashMyUnit myMessage = new Room2C_FlashMyUnit() { Units = cardInfos };
        RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
        Room2C_FlashEnemyUnit enemyMessage = new Room2C_FlashEnemyUnit() { Units = cardInfos };
        RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
    }

    public static void ToDo_AttributeAuraEffectToTarget(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, GameEvent trigger, int num) {
        RoomPlayer player = actor.GetOwner();
        CardGameComponent_Cards cards = actor.GetParent<CardGameComponent_Cards>();
        RoomCard target = null;
        Log.Warning(trigger.GameEventType.ToString());
        if (trigger.GameEventType == GameEventType.CallUnitOver) {
            target = cards.GetChild<RoomCard>(trigger.Actor);
            if (target.PlayerId != actor.PlayerId) {
                Log.Warning($"目标单位获得Buff{trigger.Actor}");
                return;
            }
        }
        target.Attack += num;
        target.HP += num;
        target.HPMax += num;
        Log.Warning(num);
        Log.Warning(target.Attack);
        Room2C_FlashUnit message = new Room2C_FlashUnit() { Units = target.RoomCard2AgentInfo() };
        RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
    }
}