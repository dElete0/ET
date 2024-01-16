namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_FlashMyUnitHandler: MessageHandler<Scene, Room2C_FlashMyUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_FlashMyUnit message)
        {
            await EventSystem.Instance.PublishAsync(root, new FlashMyUnits() {Cards = message.Units});
        }
    }
}