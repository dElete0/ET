namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CallUnit : AEvent<Scene, CallUnit>
    {
        protected override async ETTask Run(Scene scene, CallUnit args) {
            //Log.Warning($"处理占位消息{args.Card.CardId}");
            await scene.GetComponent<Room>().CallUnit(args.Card, true);
            await scene.GetComponent<Room>().OrderUnits(args.UnitsOrder, true);
        }
    }
}