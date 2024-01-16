namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_FlashEnemyUnits : AEvent<Scene, FlashEnemyUnits>
    {
        protected override async ETTask Run(Scene scene, FlashEnemyUnits args) {
            await UICGGameHelper.Room2C_FlashUnits(scene.GetComponent<Room>(), args.Cards, false);
        }
    }
}