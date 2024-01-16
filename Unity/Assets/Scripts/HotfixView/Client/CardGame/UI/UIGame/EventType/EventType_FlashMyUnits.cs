namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_FlashMyUnits : AEvent<Scene, FlashMyUnits>
    {
        protected override async ETTask Run(Scene scene, FlashMyUnits args) {
            await UICGGameHelper.Room2C_FlashUnits(scene.GetComponent<Room>(), args.Cards, true);
        }
    }
}