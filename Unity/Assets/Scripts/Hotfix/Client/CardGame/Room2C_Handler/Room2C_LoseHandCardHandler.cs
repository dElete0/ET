namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_LoseHandCardHandler: MessageHandler<Scene, Room2C_LoseHandCard>
    {
        protected override async ETTask Run(Scene root, Room2C_LoseHandCard message) {
            await EventSystem.Instance.PublishAsync(root, new LoseHandCard() {CardId = message.CardId});
        }
    }
}