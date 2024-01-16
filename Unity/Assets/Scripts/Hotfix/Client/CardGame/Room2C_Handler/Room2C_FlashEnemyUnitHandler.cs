namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_FlashEnemyUnitHandler: MessageHandler<Scene, Room2C_FlashEnemyUnit>
    {
        protected override async ETTask Run(Scene root, Room2C_FlashEnemyUnit message)
        {
            await EventSystem.Instance.PublishAsync(root, new FlashEnemyUnits() {Cards = message.Units});
        }
    }
}