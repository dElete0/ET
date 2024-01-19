namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnit : AEvent<Scene, CallUnit>
    {
        protected override async ETTask Run(Scene scene, CallUnit args) {
            UI ui = await UIHelper.Get(scene.GetComponent<Room>(), UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            //Log.Warning($"处理占位消息{args.Card.CardId}");
            await scene.GetComponent<Room>().CallUnit(uicgGameComponent, args.Card, true);
            scene.GetComponent<Room>().OrderUnits(uicgGameComponent, args.UnitsOrder, true);
        }
    }
}