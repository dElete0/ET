namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyCallUnitsHandler: MessageHandler<Scene, Room2C_EnemyCallUnits>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyCallUnits message)
        {
            // Log.Warning($"客户端收到排序消息:{message.UnitOrder.Count}");
            await EventSystem.Instance.PublishAsync(root, new EnemyCallUnits() {Card = message.Units, UnitsOrder = message.Order});
        }
    }
}