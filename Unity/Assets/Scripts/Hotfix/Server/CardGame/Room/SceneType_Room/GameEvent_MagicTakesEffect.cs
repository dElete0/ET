using System.Collections.Generic;

namespace ET.Server;
[FriendOfAttribute(typeof(ET.RoomCard))]
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
public static class GameEvent_MagicTakesEffect
{
    public static async ETTask ToDo_MagicTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player)
    {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (Power_Struct power in powerStructs)
        {
            int magicAdd = player.GetMagicAddByRoomPlayer();
            int countNum = PowerNumChangeHelper.GetCountNumByAddMagicDamage(power.PowerType);
            Power_Struct powerToDo = power;
            powerToDo = new Power_Struct(power, countNum, magicAdd);
            if (card.AttributePowers.Contains(Power_Type.Inherit))
            {
                int Inherit = player.GetInheritByBaseId(card.ConfigId);
                if (Inherit > 0)
                {
                    int powerCount = PowerNumChangeHelper.GetCountNumByInherit(power.PowerType);
                    powerToDo = new Power_Struct(powerToDo, countNum, powerCount);
                }
                else
                {
                    player.GetComponent<CardGameComponent_Player>().InheritCount.Add(card.ConfigId, 1);
                }
            }
            await powerToDo.PowerToDo(roomEventTypeComponent, eventInfo, card, target, player);
        }
    }

    public static async ETTask ToDo_PlotTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player)
    {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (var power in powerStructs)
        {
            await power.PowerToDo(roomEventTypeComponent, eventInfo, card, target, player);
        }
    }
}