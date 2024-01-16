namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOf(typeof(RoomServerComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    [FriendOfAttribute(typeof(ET.Server.RoomAIComponent))]
    public class C2Room_SelectCardHandler : MessageHandler<Scene, C2Room_SelectCard>
    {
        protected override async ETTask Run(Scene root, C2Room_SelectCard message)
        {
            await C2Room_SelectCard(root.GetComponent<Room>(), message);
        }

        public static async ETTask AI2Room_SelectCard(RoomAIComponent ai, C2Room_SelectCard message) {
            await C2Room_SelectCard(ai.GetParent<Room>(), message);
        }

        public static async ETTask C2Room_SelectCard(Room room, C2Room_SelectCard message)
        {
            RoomEventTypeComponent roomEventTypeComponent = room.GetComponent<RoomEventTypeComponent>();
            CGServerUpdater serverUpdater = room.GetComponent<CGServerUpdater>();
            RoomPlayer roomPlayer = room.GetComponent<RoomServerComponent>().GetChild<RoomPlayer>(message.PlayerId);
            if (serverUpdater.NowPlayer == roomPlayer.Id)
            {
                room.GetComponent<ObjectWait>().Notify(new WaitType.Wait_C2Room_Select() {Message = message});
            }
            await ETTask.CompletedTask;
        }
    }
}