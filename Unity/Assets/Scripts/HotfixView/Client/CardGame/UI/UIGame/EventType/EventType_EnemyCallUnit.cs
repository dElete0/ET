namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCallUnit : AEvent<Scene, EnemyCallUnit>
    {
        protected override async ETTask Run(Scene scene, EnemyCallUnit args) {
            UI ui = await UIHelper.Get(scene.GetComponent<Room>(), UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await scene.GetComponent<Room>().CallUnit(uicgGameComponent, args.Card, false);
            scene.GetComponent<Room>().OrderUnits(uicgGameComponent, args.UnitsOrder, false);
        }
    }
}