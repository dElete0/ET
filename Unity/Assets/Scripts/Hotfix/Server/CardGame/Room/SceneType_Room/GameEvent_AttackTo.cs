namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_AttackTo
    {
        public static void ToDo_AttackTo(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target) {
            card.AttackCount--;
            if (card.Attack > 0) roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, card, target, card.Attack));
            if (target.Attack > 0) roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, target, card, target.Attack));
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttackOver(roomEventTypeComponent, card, target));

            // Todo 客户端执行攻击动作
        }
    }
}