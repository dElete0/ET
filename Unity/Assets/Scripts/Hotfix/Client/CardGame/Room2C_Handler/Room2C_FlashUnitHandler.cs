namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_FlashUnitHandler: MessageHandler<Scene, Room2C_FlashUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_FlashUnit message)
        {
            await EventSystem.Instance.PublishAsync(root, new FlashUnit() {Card = message.Unit});
        }
    }
}