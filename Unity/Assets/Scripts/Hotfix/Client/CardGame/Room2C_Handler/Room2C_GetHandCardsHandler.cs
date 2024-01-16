namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GetHandCardsHandler: MessageHandler<Scene, Room2C_GetHandCards>
    {
        protected override async ETTask Run(Scene root, Room2C_GetHandCards message)
        {
            await EventSystem.Instance.PublishAsync(root, new GetHandCards() {Cards = message.Cards});
        }
    }
}