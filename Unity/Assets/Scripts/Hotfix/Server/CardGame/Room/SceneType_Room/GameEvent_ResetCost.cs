namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_ResetCost
    {
        public static async ETTask ToDo_ResetCost(this RoomEventTypeComponent room, RoomPlayer player) {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            playerInfo.Cost = playerInfo.CostTotal;
        }
    }
}