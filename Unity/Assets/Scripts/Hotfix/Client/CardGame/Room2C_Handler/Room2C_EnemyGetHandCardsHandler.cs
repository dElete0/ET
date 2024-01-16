namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyGetHandCardsHandler: MessageHandler<Scene, Room2C_EnemyGetHandCards>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyGetHandCards message)
        {
            await EventSystem.Instance.PublishAsync(root, new EnemyGetHandCards() {Cards = message.Cards});
        }
    }
}