namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Damage
    {
        public static void ToDo_HeroDamage(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target, int count)
        {
            target.HP -= count;
            if (target.HP < 0) {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Dead(roomEventTypeComponent, target));
            }
            
            // Todo 客户端执行受伤动作
            Room room = target.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
        }

        public static void ToDo_UnitDamage(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = target.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
            
            if (target.HP < 0) {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Dead(roomEventTypeComponent, card));
            }
        }

        public static void ToDo_AgentDamage(this RoomEventTypeComponent roomEventTypeComponent, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            if (target.HP < 0) {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.Dead(roomEventTypeComponent, card));
            }
            
            // Todo 客户端执行受伤动作
            Room room = target.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
        }
    }
}