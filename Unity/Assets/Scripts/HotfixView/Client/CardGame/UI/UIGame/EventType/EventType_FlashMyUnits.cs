namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_FlashUnits : AEvent<Scene, FlashUnits>
    {
        protected override async ETTask Run(Scene scene, FlashUnits args) {
            await UICGGameHelper.Room2C_FlashUnits(scene.GetComponent<Room>(), args.Cards);
        }
    }
}