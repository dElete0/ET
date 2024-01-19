namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_TargetGetAttribute
    {
        public static async ETTask ToDo_TargetGetAttribute(this RoomEventTypeComponent room, RoomCard actor, RoomCard target, int num)
        {
            await ETTask.CompletedTask;
            target.Attack += num;
            target.HP += num;
            target.HPMax += num;

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Units = target.RoomCard2UnitInfo() };
        }
    }
}