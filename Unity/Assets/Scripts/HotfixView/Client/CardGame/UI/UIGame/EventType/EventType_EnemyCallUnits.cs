namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCallUnits : AEvent<Scene, EnemyCallUnits>
    {
        protected override async ETTask Run(Scene scene, EnemyCallUnits args) {
            UI ui = await UIHelper.Get(scene.GetComponent<Room>(), UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await scene.GetComponent<Room>().CallUnits(uicgGameComponent, args.Card, false);
            scene.GetComponent<Room>().OrderUnits(uicgGameComponent, args.UnitsOrder, false);
        }
    }
}