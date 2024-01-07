namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnterGameRoomHandler: MessageHandler<Scene, Room2C_CGStart>
    {
        protected override async ETTask Run(Scene root, Room2C_CGStart message)
        {
            root.GetComponent<ObjectWait>().Notify(new WaitType.Wait_Room2C_GameStart() {Message = message});
            await ETTask.CompletedTask;
        }
    }
}