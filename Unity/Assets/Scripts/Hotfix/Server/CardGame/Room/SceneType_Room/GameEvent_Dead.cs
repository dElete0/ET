namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Dead
    {
        public static void ToDo_Dead(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card)
        {
            switch (card.CardType) {
                case CardType.Hero:
                    // Todo 游戏结束

                    // Todo 客户端游戏结束
                    
                    break;
                case CardType.Unit:
                    // Todo Unit里移除这项
                    Room room = roomEventTypeComponent.GetParent<Room>();
                    Room2C_UnitDead unitDead = new Room2C_UnitDead() { CardId = card.Id };
                    RoomMessageHelper.BroadCast(room, unitDead);
                    room.GetComponent<CardGameComponent_Cards>().RemoveCard(card);
                    break;
            }
        }
    }
}