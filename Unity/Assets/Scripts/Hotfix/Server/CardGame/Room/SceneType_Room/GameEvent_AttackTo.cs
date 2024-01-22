namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static class GameEvent_AttackTo
    {
        public static async ETTask ToDo_AttackTo(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target)
        {
            card.AttackCount--;
            // Todo 客户端执行攻击动作
            Room2C_Attack attack = new Room2C_Attack() { Actor = card.Id, Target = target.Id };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), attack);

            if (card.Attack > 0) await roomEventTypeComponent.BroadEvent(GameEventFactory.Damage(roomEventTypeComponent, card, target, card.Attack), eventInfo);
            if (target.Attack > 0) await roomEventTypeComponent.BroadEvent(GameEventFactory.Damage(roomEventTypeComponent, target, card, target.Attack), eventInfo);
            await roomEventTypeComponent.BroadEvent(GameEventFactory.AttackOver(roomEventTypeComponent, card, target), eventInfo);
        }

        public static async ETTask ToDo_TargetGetAttackThisTurn(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard target, int num)
        {
            target.Attack += num;
            target.GetComponent<CardEventTypeComponent>().UnitGameEventTypes.Add(TriggerEventFactory.TurnOver(), GameEventFactory.LoseAttack(roomEventTypeComponent, actor, target, num));

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = target.RoomCard2UnitInfo() };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
        }

        public static async ETTask ToDo_LoseAttack(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor,
        RoomCard target, int num)
        {
            target.Attack -= num;

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = target.RoomCard2UnitInfo() };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
        }
    }
}