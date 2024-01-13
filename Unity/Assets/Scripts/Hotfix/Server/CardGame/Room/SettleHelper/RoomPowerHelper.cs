namespace ET.Server {
    public static class RoomPowerHelper {
        public static void GetHandCardFromGroup(this Power_Struct arrange, RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, int count) {
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player, count), eventInfo);
        }
    }
}