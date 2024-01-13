namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CallUnitHandler: MessageHandler<Scene, Room2C_CallUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_CallUnit message)
        {
            // Log.Warning($"客户端收到排序消息:{message.UnitOrder.Count}");
            await EventSystem.Instance.PublishAsync(root, new CallUnit() {Card = message.Card, UnitsOrder = message.UnitOrder});
        }
    }
}

