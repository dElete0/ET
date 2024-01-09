namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_AttackTo
    {
        public static void ToDo_AttackTo(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target) {
            card.AttackCount--;
            // Todo 客户端执行攻击动作
            Room2C_Attack attack = new Room2C_Attack(){Actor = card.Id, Target = target.Id};
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), attack);

            if (card.Attack > 0) roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, card, target, card.Attack));
            if (target.Attack > 0) roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Damage(roomEventTypeComponent, target, card, target.Attack));
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttackOver(roomEventTypeComponent, card, target));
        }
    }
}