namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GetHandCardFromShowCardHandler: MessageHandler<Scene, Room2C_GetHandCardFromShowCard>
    {
        protected override async ETTask Run(Scene root, Room2C_GetHandCardFromShowCard message)
        {
            
            await ETTask.CompletedTask;
        }
    }
}