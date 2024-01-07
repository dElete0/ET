namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyNewAgent : AEvent<Scene, EnemyNewAgentType>
    {
        protected override async ETTask Run(Scene scene, EnemyNewAgentType args) {
            await UICGGameHelper.Room2C_NewAgentType(scene.GetComponent<Room>(), args.Agent1, args.Agent2, false);
        }
    }
}