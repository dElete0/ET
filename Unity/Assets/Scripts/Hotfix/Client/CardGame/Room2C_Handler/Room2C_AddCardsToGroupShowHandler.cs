namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_AddCardsToGroupShowHandler: MessageHandler<Scene, Room2C_AddCardsToGroupShow>
    {
        protected override async ETTask Run(Scene root, Room2C_AddCardsToGroupShow message)
        {
            await EventSystem.Instance.PublishAsync(root, new AddCardsToGroupShow() {Cards = message.Cards, IsMy = message.IsMy});
        }
    }
}