namespace ET.Server {
    [EntitySystemOf(typeof(RoomPlayer))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static partial class RoomPlayerSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RoomPlayer self, long id) {
            self.PlayerId = id;
        }
    }
}