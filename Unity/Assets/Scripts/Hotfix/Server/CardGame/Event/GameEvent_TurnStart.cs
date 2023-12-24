namespace ET.Server {
    [Event(SceneType.Map)]
    public class GameEvent_TurnStart : AEvent<Scene, GameEventType_TurnStart> {
        protected override async ETTask Run(Scene scene, GameEventType_TurnStart a) {
            await ETTask.CompletedTask;
        }
    }
}