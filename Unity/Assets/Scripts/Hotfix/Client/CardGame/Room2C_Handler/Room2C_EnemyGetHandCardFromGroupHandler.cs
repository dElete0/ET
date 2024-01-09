namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyGetHandCardFromGroupHandler: MessageHandler<Scene, Room2C_EnemyGetHandCardFromGroup>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyGetHandCardFromGroup message)
        {
            await EventSystem.Instance.PublishAsync(root, new EnemyGetHandCardFromGroup() {Card = message.CardInfo});
            await ETTask.CompletedTask;
        }
    }
}