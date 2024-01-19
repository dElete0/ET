namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_RemoveCardFromGroup
    {
        public static async ETTask ToDo_RemoveCardFromGroup(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, long card)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            playerInfo.Groups.Remove(card);

            //客户端牌库总量发生变化
        }
    }
}