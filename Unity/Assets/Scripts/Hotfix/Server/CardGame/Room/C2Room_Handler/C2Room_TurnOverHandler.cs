namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOf(typeof(RoomServerComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    [FriendOfAttribute(typeof(ET.Server.RoomAIComponent))]
    public class C2Room_TurnOverHandler : MessageHandler<Scene, C2Room_TurnOver>
    {
        protected override async ETTask Run(Scene root, C2Room_TurnOver message)
        {
            await C2Room_TurnOver(root.GetComponent<Room>(), message);
        }

        public static async ETTask AI2Room_TurnOver(RoomAIComponent ai, C2Room_TurnOver message) {
            await C2Room_TurnOver(ai.GetParent<Room>(), message);
        }

        public static async ETTask C2Room_TurnOver(Room room, C2Room_TurnOver message)
        {
            RoomEventTypeComponent roomEventTypeComponent = room.GetComponent<RoomEventTypeComponent>();
            CGServerUpdater serverUpdater = room.GetComponent<CGServerUpdater>();
            RoomPlayer roomPlayer = room.GetComponent<RoomServerComponent>().GetChild<RoomPlayer>(message.PlayerId);
            if (serverUpdater.NowPlayer == roomPlayer.Id)
            {
                Log.Warning("回合结束：" + roomPlayer.Id);
                await roomEventTypeComponent.Event_TurnOver(roomPlayer);
            }
            await ETTask.CompletedTask;
        }
    }
}