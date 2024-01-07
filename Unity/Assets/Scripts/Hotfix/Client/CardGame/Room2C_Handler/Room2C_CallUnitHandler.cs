namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CallUnitHandler: MessageHandler<Scene, Room2C_CallUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_CallUnit message)
        {
            await EventSystem.Instance.PublishAsync(root, new CallUnit() {Card = message.Card});
            await ETTask.CompletedTask;
        }
    }
}

