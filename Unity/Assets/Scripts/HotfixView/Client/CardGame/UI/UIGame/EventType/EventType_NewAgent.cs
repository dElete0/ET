namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_NewAgent : AEvent<Scene, NewAgentType>
    {
        protected override async ETTask Run(Scene scene, NewAgentType args) {
            Log.Warning("创建干员");
            await UICGGameHelper.Room2C_NewAgentType(scene.GetComponent<Room>(), args.Agent1, args.Agent2, true);
        }
    }
}