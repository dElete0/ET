namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_UnitDeadHandler : MessageHandler<Scene, Room2C_UnitDead>
    {
        protected override async ETTask Run(Scene root, Room2C_UnitDead message)
        {
            await EventSystem.Instance.PublishAsync(root, new UnitDead() { CardIds = message.CardIds} );

            await ETTask.CompletedTask;
        }
    }
}