namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_GetHandCard
    {
        public static void ToDo_GetHandCard(this RoomEventTypeComponent room, RoomPlayer player, long card) {
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            Log.Debug("展示的卡加入手牌");
            playerInfo.HandCards.Add(card);
            
            RoomCard roomCard = room.GetParent<Room>().GetComponent<CardGameComponent_Cards>().GetChild<RoomCard>(card);
            
            // Client
            // 通知客户端将抽到的展示牌置入手牌
            Room2C_EnemyGetHandCardFromShowCard room2C_EnemyGetHandCardFromShowCard = new() { CardId = card };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2C_EnemyGetHandCardFromShowCard);

            Room2C_GetHandCardFromShowCard room2C_GetHandCardFromShowCard = new() { CardId = card };
            RoomMessageHelper.ServerSendMessageToClient(player, room2C_GetHandCardFromShowCard);
        }
    }
}