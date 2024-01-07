namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyGroupCountHandler: MessageHandler<Scene, Room2C_EnemyGroupCount> {
        protected override async ETTask Run(Scene root, Room2C_EnemyGroupCount message) {
            
            await ETTask.CompletedTask;
        }
    }
}