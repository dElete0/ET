namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CostHandler: MessageHandler<Scene, Room2C_Cost>
    {
        protected override async ETTask Run(Scene root, Room2C_Cost message)
        {
            if (message.IsMy) {
                await EventSystem.Instance.PublishAsync(root, new MyCost() {Cost = message.Now, CostMax = message.Max});
            } else {
                await EventSystem.Instance.PublishAsync(root, new EnemyCost() {Cost = message.Now, CostMax = message.Max});
            }
        }
    }
}