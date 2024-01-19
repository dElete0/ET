namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GetArmorHandler: MessageHandler<Scene, Room2C_GetArmor>
    {
        protected override async ETTask Run(Scene root, Room2C_GetArmor message)
        {
            await EventSystem.Instance.PublishAsync(root, new GetArmor() {Num = message.Num, IsMy = message.IsMy});
        }
    }
}