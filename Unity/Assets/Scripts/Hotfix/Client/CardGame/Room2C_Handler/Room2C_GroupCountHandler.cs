namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GroupCountHandler: MessageHandler<Scene, Room2C_GroupCount> {
        protected override async ETTask Run(Scene root, Room2C_GroupCount message) {
            
            await ETTask.CompletedTask;
        }
    }
}