namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Dead
    {
        public static void ToDo_Dead(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card)
        {
            switch (card.CardType) {
                case CardType.Hero:
                    // Todo 游戏结束
                    break;
                case CardType.Unit:
                    // Todo Unit里移除这项
                    break;
            }

            // Todo 客户端游戏结束
        }
    }
}