namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyCallUnitHandler: MessageHandler<Scene, Room2C_EnemyCallUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyCallUnit message)
        {
            await EventSystem.Instance.PublishAsync(root, new EnemyCallUnit() {Card = message.Card});
        }
    }
}