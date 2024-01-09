namespace ET.Server;
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
public static class GameEvent_LoseHandCard
{
    public static void ToDo_LoseHandCard(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, RoomCard card)
    {
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        playerInfo.HandCards.Remove(card.Id);

        //发送消息
        Room2C_LoseHandCard loseHandCard = new() { CardId = card.Id };
        RoomMessageHelper.BroadCast(player.GetParent<Room>(), loseHandCard);
    }
}