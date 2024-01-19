namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_AddCardsToGroupHideHandler: MessageHandler<Scene, Room2C_AddCardsToGroupHide>
    {
        protected override async ETTask Run(Scene root, Room2C_AddCardsToGroupHide message)
        {
            await EventSystem.Instance.PublishAsync(root, new AddCardsToGroupHide() {Num = message.Num, IsMy = message.IsMy});
        }
    }
}