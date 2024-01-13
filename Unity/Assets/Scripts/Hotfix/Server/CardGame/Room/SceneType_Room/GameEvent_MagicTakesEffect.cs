using System.Collections.Generic;

namespace ET.Server;
public static class GameEvent_MagicTakesEffect
{
    public static void ToDo_MagicTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (Power_Struct power in powerStructs) {
            power.Card1 = card;
            power.Card2 = target;
            power.RoomPlayer1 = player;
            power.PowerToDo(roomEventTypeComponent, eventInfo);
        }
    }
    
    public static void ToDo_PlotTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (var power in powerStructs) {
            power.Card1 = card;
            power.Card2 = target;
            power.RoomPlayer1 = player;
            power.PowerToDo(roomEventTypeComponent, eventInfo);
        }
    }
}