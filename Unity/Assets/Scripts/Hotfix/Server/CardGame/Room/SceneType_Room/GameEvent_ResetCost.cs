namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_ResetCost
    {
        public static void ToDo_ResetCost(this RoomEventTypeComponent room, RoomPlayer player) {
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            playerInfo.Cost = playerInfo.CostTotal;
        }
    }
}