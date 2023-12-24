namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_CreateRoomHandler: MessageHandler<Scene, M2C_CreateRoom> {
        protected override async ETTask Run(Scene root, M2C_CreateRoom message) {

            await RoomChangeHelper.RoomChangeTo(root, message.SceneName, message.SceneInstanceId);
        }
    }
}