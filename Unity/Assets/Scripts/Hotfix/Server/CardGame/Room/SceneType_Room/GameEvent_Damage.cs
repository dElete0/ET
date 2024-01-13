namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Damage
    {
        public static void ToDo_HeroDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count)
        {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
            
            
            if (target.HP < 1) {
                eventInfo.DeadList.Add(target);
            }
        }

        public static void ToDo_UnitDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
            
            if (target.HP < 1) {
                Log.Warning($"添加死亡单位:{target.Id}");
                eventInfo.DeadList.Add(target);
            }
        }

        public static void ToDo_AgentDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);

            if (target.HP < 1) {
                eventInfo.DeadList.Add(target);
            }
        }
    }
}