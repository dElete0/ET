namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_GetHandCardFromGroupHandler: MessageHandler<Scene, Room2C_GetHandCardFromGroup>
    {
        protected override async ETTask Run(Scene root, Room2C_GetHandCardFromGroup message)
        {
            await EventSystem.Instance.PublishAsync(root, new GetHandCardFromGroup() {Card = message.CardInfo});
            await ETTask.CompletedTask;
        }
    }
}