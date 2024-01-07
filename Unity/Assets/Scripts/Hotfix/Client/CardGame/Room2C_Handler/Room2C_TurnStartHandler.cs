namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_TurnStartHandler : MessageHandler<Scene, Room2C_TurnStart>
    {
        protected override async ETTask Run(Scene root, Room2C_TurnStart message)
        {
            await EventSystem.Instance.PublishAsync(root, new TurnStart() {
                IsThisClient = message.IsThisClient,
                Cost = message.Cost,
                CostD = message.CostD,
                Red = message.Red,
                Black = message.Black,
                Blue = message.Blue,
                Green = message.Green,
                Grey = message.Grey,
                White = message.White,
            } );

            await ETTask.CompletedTask;
        }
    }
}