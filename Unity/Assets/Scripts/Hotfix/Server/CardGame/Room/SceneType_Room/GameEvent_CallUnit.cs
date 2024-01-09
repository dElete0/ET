namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_CallUnit
    {
        public static void ToDo_CallUnit(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomPlayer player, int pos)
        {
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            playerCards.Units.Insert(pos, card.Id);
            card.IsCallThisTurn = true;

            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.UnitBeCalled(roomEventTypeComponent, card, player));

            // Todo 客户端执行创建动作
            Room2C_CallUnit room2CCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = playerCards.Units };
            RoomMessageHelper.ServerSendMessageToClient(player, room2CCallUnit);

            Room2C_EnemyCallUnit room2CEnemyCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = playerCards.Units };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2CEnemyCallUnit);
        }
    }
}
