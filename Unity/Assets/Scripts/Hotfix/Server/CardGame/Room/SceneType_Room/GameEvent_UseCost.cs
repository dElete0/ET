using System;
using System.Collections.Generic;

namespace ET.Server;
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
[FriendOfAttribute(typeof(ET.RoomCard))]
public static class GameEvent_UseCost
{
    public static async ETTask ToDo_UseCost(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, int cost)
    {
        await ETTask.CompletedTask;
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        playerInfo.Cost -= cost;

        //发送消息
        Room2C_Cost MyCost = new Room2C_Cost() { Now = playerInfo.Cost, Max = playerInfo.CostTotal, IsMy = true };
        RoomMessageHelper.ServerSendMessageToClient(player, MyCost);
        Room2C_Cost EnemyCost = new Room2C_Cost() { Now = playerInfo.Cost, Max = playerInfo.CostTotal, IsMy = false };
        RoomMessageHelper.BroadCastWithOutPlayer(player, EnemyCost);
    }

    public static async ETTask ToDo_UseCard(this RoomEventTypeComponent room, EventInfo info, RoomPlayer player, RoomCard card, RoomCard target, int pos) {
        Room2C_ShowUseCard myMessage = new Room2C_ShowUseCard() { Card = card.RoomCard2MyHandCardInfo() , IsMy = true};
        Room2C_ShowUseCard enemyMessage = new Room2C_ShowUseCard() { Card = card.RoomCard2MyHandCardInfo() , IsMy = false};
        RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
        RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
        await room.ToDo_LoseHandCard(player, card);
        await room.ToDo_UseCost(player, card.Cost);
        if (card.CardType == CardType.Unit)
        {
            await room.BroadEvent(GameEventFactory.UseUnitCard(room, player, card, target, pos), info);
        }
        else if (card.CardType == CardType.Magic)
        {
            await room.BroadEvent(GameEventFactory.UseMagicCard(room, player, card, target), info);
        }
        else if (card.CardType == CardType.Plot)
        {
            await room.BroadEvent(GameEventFactory.UsePlotCard(room, player, card, target), info);
        }
    }

    public static async ETTask ToDo_UseUnitCard(this RoomEventTypeComponent room, EventInfo info, RoomPlayer player, RoomCard card, RoomCard target, int pos) {
        //先占位，以免战吼期间导致友方单位死亡，位置错误
        await room.ToDo_UnitStand(player, card, pos);
        if (card.GetArranges().Count > 0)
        {
            await room.BroadEvent(GameEventFactory.UnitArrange(room, card, target, player), info);
        }

        if (card.GetAura().Count > 0) {
            await room.BroadEvent(GameEventFactory.AuraEffect(room, card, player), info);
        }
        await room.ToDo_UnitInFight(info, player, card);
    }

    public static async ETTask ToDo_UseMagicCard(this RoomEventTypeComponent room, EventInfo info, RoomPlayer player, RoomCard card, RoomCard target) {
        //if (target != null) Log.Warning(target.Name);
        //await ETTask.CompletedTask;
        if (card.AttributePowers.Contains(Power_Type.Risk)) {
            bool randomBool = new Random().NextDouble() > 0.5;
            if (randomBool) {
                Room2C_RiskSuccess message = new Room2C_RiskSuccess() { Card = card.RoomCard2MyHandCardInfo(), IsRiskSuccess = false };
                RoomMessageHelper.BroadCast(room.GetParent<Room>(), message);
                
                await room.BroadEvent(GameEventFactory.MagicTakesEffect(room, card, target, player), info);
                await room.BroadEvent(GameEventFactory.MagicTakesEffect(room, card, target, player), info);
            } else {
                Room2C_RiskSuccess message = new Room2C_RiskSuccess() { Card = card.RoomCard2MyHandCardInfo(), IsRiskSuccess = false };
                RoomMessageHelper.BroadCast(room.GetParent<Room>(), message);
            }
        } else {
            await room.BroadEvent(GameEventFactory.MagicTakesEffect(room, card, target, player), info);
        }
    }
    
    public static async ETTask ToDo_PlotTakesEffect(this RoomEventTypeComponent room, EventInfo info, RoomPlayer player, RoomCard card, RoomCard target) {
        await room.BroadEvent(GameEventFactory.PlotTakesEffect(room, card, target, player), info);
    }
}