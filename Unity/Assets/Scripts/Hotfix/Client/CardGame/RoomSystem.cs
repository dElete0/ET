namespace ET.Client {

    [EntitySystemOf(typeof(GameRoom))]
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    public static partial class RoomSystem
    {
        [EntitySystem]
        private static void Awake(this ET.GameRoom self, ET.GameRoomType args2)
        {
            Log.Debug("Client: 创建了一个房间");
        }
        [EntitySystem]
        private static void Update(this ET.GameRoom self)
        {
        }
    }
}
