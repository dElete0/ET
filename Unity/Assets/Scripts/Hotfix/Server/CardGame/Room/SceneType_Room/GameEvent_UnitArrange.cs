using System.Collections.Generic;

namespace ET.Server;
public static class GameEvent_UnitArrange
{
    public static void ToDo_UnitArrange(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetArranges();
        foreach (var arrange in powerStructs) {
            arrange.PowerToDo(roomEventTypeComponent, card, target, player);
        }
    }
}