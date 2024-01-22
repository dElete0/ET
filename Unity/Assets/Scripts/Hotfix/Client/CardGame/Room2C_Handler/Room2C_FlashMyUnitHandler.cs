namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_FlashUnitsHandler: MessageHandler<Scene, Room2C_FlashUnits>
    {
        protected override async ETTask Run(Scene root, Room2C_FlashUnits message)
        {
            await EventSystem.Instance.PublishAsync(root, new FlashUnits() {Cards = message.Units});
        }
    }
}