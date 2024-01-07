namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyGetHandCardFromShowCardHandler: MessageHandler<Scene, Room2C_EnemyGetHandCardFromShowCard>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyGetHandCardFromShowCard message)
        {
            
            await ETTask.CompletedTask;
        }
    }
}