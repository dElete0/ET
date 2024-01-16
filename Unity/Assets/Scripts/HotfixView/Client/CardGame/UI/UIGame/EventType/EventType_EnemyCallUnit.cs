namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCallUnit : AEvent<Scene, EnemyCallUnit>
    {
        protected override async ETTask Run(Scene scene, EnemyCallUnit args) {
            await scene.GetComponent<Room>().CallUnit(args.Card, false);
            await scene.GetComponent<Room>().OrderUnits(args.UnitsOrder, false);
        }
    }
}