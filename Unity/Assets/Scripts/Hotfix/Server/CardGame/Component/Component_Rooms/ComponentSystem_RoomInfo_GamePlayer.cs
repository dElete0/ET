namespace ET.Server {
    [EntitySystemOf(typeof(Component_Room_GamePlayer))]
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    public static partial class ComponentSystem_RoomInfo_GamePlayer
    {
        [EntitySystem]
        private static void Awake(this ET.Component_Room_GamePlayer self, int args2) {
            self.PlayerMax = args2;
            Log.Debug("房间最大人数：" + args2);
        }
    }
}