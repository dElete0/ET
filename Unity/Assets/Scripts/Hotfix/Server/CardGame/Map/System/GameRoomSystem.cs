namespace ET.Server {
    [EntitySystemOf(typeof(GameRoom))]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    public static partial class GameRoomSystem
    {
        [EntitySystem]
        private static void Awake(this ET.GameRoom self, ET.GameRoomType args2) {
            self.type = args2;
            self.AddComponent<Component_RoomInfo>();
        }
        [EntitySystem]
        private static void Update(this ET.GameRoom self)
        {

        }
    }
}