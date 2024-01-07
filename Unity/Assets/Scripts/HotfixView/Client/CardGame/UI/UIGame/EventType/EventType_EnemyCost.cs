namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCost : AEvent<Scene, EnemyCost>
    {
        protected override async ETTask Run(Scene scene, EnemyCost args) {
            await UICGGameHelper.Room2Cost_EnemyCost(scene.GetComponent<Room>(), args.Cost, args.CostMax);
        }
    }
}