using System.Collections.Generic;

namespace ET.Server;
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
public static class GameEvent_UseCost
{
    public static void ToDo_UseCost(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, int cost)
    {
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        playerInfo.Cost -= cost;
        
        //发送消息
        Room2C_Cost MyCost = new Room2C_Cost() { Now = playerInfo.Cost, Max = playerInfo.CostTotal, IsMy = true };
        RoomMessageHelper.ServerSendMessageToClient(player, MyCost);
        Room2C_Cost EnemyCost = new Room2C_Cost() { Now = playerInfo.Cost, Max = playerInfo.CostTotal, IsMy = false };
        RoomMessageHelper.BroadCastWithOutPlayer(player, EnemyCost);
    }
}