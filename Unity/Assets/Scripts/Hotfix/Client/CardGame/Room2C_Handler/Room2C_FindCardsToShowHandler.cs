namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_FindCardsToShowHandler: MessageHandler<Scene, Room2C_FindCardsToShow>
    {
        protected override async ETTask Run(Scene root, Room2C_FindCardsToShow message)
        {
            await EventSystem.Instance.PublishAsync(root, new FindCardsToShow() {Cards = message.Cards});
        }
    }
}