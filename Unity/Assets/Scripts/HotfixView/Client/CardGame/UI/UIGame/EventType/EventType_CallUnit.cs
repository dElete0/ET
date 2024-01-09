namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnit : AEvent<Scene, CallUnit>
    {
        protected override async ETTask Run(Scene scene, CallUnit args) {
            await UICGGameHelper.Room2C_CallUnit(scene.GetComponent<Room>(), args.Card, true);
            await UICGGameHelper.OrderUnits(scene.GetComponent<Room>(), args.UnitsOrder);
        }
    }
}