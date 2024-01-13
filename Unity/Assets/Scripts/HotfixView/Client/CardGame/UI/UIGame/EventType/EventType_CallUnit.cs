namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnit : AEvent<Scene, CallUnit>
    {
        protected override async ETTask Run(Scene scene, CallUnit args) {
            await scene.GetComponent<Room>().Room2C_CallUnit(args.Card, true);
            await scene.GetComponent<Room>().OrderUnits(args.UnitsOrder, true);
        }
    }
}