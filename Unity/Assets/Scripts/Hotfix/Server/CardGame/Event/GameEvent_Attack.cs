namespace ET.Server {
    [Event(SceneType.Map)]
    public class GameEvent_Attack : AEvent<Scene, GameEventType_Attack> {
        protected override async ETTask Run(Scene scene, GameEventType_Attack a) {

            Log.Debug(a.Actor.ToString() + "攻击了" + a.Target.ToString());
            await ETTask.CompletedTask;
        }
    }
}