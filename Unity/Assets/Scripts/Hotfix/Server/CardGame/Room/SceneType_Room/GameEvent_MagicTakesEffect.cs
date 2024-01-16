using System.Collections.Generic;

namespace ET.Server;
public static class GameEvent_MagicTakesEffect
{
    public static void ToDo_MagicTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (Power_Struct power in powerStructs) {
            power.PowerToDo(roomEventTypeComponent, eventInfo, card, target, player);
        }
    }
    
    public static void ToDo_PlotTakesEffect(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetRelease();
        foreach (var power in powerStructs) {
            power.PowerToDo(roomEventTypeComponent, eventInfo, card, target, player);
        }
    }
}