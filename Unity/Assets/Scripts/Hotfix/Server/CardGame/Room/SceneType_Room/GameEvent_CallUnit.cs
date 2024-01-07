namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_CallUnit
    {
        public static void ToDo_CallUnit(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomPlayer player)
        {
            Log.Warning("召唤单位");
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            playerCards.Units.Add(card.Id);
            
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.UnitBeCalled(roomEventTypeComponent, card, player));

            // Todo 客户端执行创建动作
            Room2C_CallUnit room2CCallUnit = new() { Card = card.RoomCard2UnitInfo() };
            RoomMessageHelper.ServerSendMessageToClient(player, room2CCallUnit);

            Room2C_EnemyCallUnit room2CEnemyCallUnit = new Room2C_EnemyCallUnit() { Card = card.RoomCard2UnitInfo() };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2CEnemyCallUnit);
        }
    }
}
