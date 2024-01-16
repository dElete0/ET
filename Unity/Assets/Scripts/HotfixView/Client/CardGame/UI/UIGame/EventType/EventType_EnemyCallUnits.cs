namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCallUnits : AEvent<Scene, EnemyCallUnits>
    {
        protected override async ETTask Run(Scene scene, EnemyCallUnits args) {
            await scene.GetComponent<Room>().CallUnits(args.Card, false);
            await scene.GetComponent<Room>().OrderUnits(args.UnitsOrder, false);
        }
    }
}