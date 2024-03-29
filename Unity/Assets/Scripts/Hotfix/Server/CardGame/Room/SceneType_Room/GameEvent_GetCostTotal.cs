namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_GetCostTotal
    {
        public static async ETTask ToDo_GetCostTotal(this RoomEventTypeComponent room, RoomPlayer player, int i) {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            playerInfo.CostTotal += i;
            if (playerInfo.CostTotal > playerInfo.CostMax) {
                playerInfo.CostTotal = playerInfo.CostMax;
            }
        }
    }
}