namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_MyCost : AEvent<Scene, MyCost>
    {
        protected override async ETTask Run(Scene scene, MyCost args) {
            await UICGGameHelper.Room2Cost_MyCost(scene.GetComponent<Room>(), args.Cost, args.CostMax);
        }
    }
}