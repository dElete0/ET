namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_MyGroupCountHandler: MessageHandler<Scene, Room2C_MyGroupCount>
    {
        protected override async ETTask Run(Scene root, Room2C_MyGroupCount message)
        {
            
            await ETTask.CompletedTask;
        }
    }
}