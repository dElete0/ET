namespace ET.Server {
    [Event(SceneType.Map)]
    public class GameEvent_GameStartOver : AEvent<Scene, GameEventType_GameStartOver> {
        protected override async ETTask Run(Scene scene, GameEventType_GameStartOver a) {
            Log.Debug("向玩家发送当前游戏的状态消息");
            
            await ETTask.CompletedTask;
        }
    }
}