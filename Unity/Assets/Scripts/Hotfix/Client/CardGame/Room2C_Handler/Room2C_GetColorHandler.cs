namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GetColorHandler: MessageHandler<Scene, Room2C_GetColor>
    {
        protected override async ETTask Run(Scene root, Room2C_GetColor message)
        {
            await EventSystem.Instance.PublishAsync(root, new GetColor() {Color = (CardColor)message.Color, Num = message.Num, IsMy = message.IsMy});
        }
    }
}