namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_RemoveCardFromGroup
    {
        public static void ToDo_RemoveCardFromGroup(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, long card)
        {
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            playerInfo.Groups.Remove(card);

            //客户端牌库总量发生变化
        }
    }
}