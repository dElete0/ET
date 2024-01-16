namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnits : AEvent<Scene, CallUnits>
    {
        protected override async ETTask Run(Scene scene, CallUnits args) {
            await scene.GetComponent<Room>().CallUnits(args.Card, true);
            await scene.GetComponent<Room>().OrderUnits(args.UnitsOrder, true);
        }
    }
}