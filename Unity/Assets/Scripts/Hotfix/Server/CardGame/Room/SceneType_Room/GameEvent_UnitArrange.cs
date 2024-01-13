using System.Collections.Generic;

namespace ET.Server;
public static class GameEvent_UnitArrange
{
    public static void ToDo_UnitArrange(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player) {
        List<Power_Struct> powerStructs = card.GetArranges();
        foreach (var power in powerStructs) {
            power.Card1 = card;
            power.Card2 = target;
            power.RoomPlayer1 = player;
            power.PowerToDo(roomEventTypeComponent, eventInfo);
        }
    }
}