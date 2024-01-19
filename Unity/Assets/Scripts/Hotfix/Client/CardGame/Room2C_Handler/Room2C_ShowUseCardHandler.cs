namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_ShowUseCardHandler : MessageHandler<Scene, Room2C_ShowUseCard>
    {
        protected override async ETTask Run(Scene root, Room2C_ShowUseCard message) {
            await EventSystem.Instance.PublishAsync(root, new ShowUseCard() {Card = message.Card, IsMy = message.IsMy});
        }
    }
}