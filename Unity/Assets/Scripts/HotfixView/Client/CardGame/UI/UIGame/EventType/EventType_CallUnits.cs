namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnits : AEvent<Scene, CallUnits>
    {
        protected override async ETTask Run(Scene scene, CallUnits args) {
            UI ui = await UIHelper.Get(scene.GetComponent<Room>(), UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await scene.GetComponent<Room>().CallUnits(uicgGameComponent, args.Card, true);
            scene.GetComponent<Room>().OrderUnits(uicgGameComponent, args.UnitsOrder, true);
        }
    }
}