namespace ET.Server {
    [Event(SceneType.Map)]
    public class GameEvent_TurnOver : AEvent<Scene, GameEventType_TurnOver> {
        protected override async ETTask Run(Scene scene, GameEventType_TurnOver a) {
            await ETTask.CompletedTask;
        }
    }
}