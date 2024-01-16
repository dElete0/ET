namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CallUnitsHandler: MessageHandler<Scene, Room2C_CallUnits>
    {
        protected override async ETTask Run(Scene root, Room2C_CallUnits message)
        {
            // Log.Warning($"客户端收到排序消息:{message.UnitOrder.Count}");
            await EventSystem.Instance.PublishAsync(root, new CallUnits() {Card = message.Units, UnitsOrder = message.Order});
        }
    }
}