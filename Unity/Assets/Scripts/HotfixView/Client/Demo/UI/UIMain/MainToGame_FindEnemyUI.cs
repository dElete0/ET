namespace ET.Client {
    [Event(SceneType.Demo)]
    public class MainToGame_FindEnemyUI: AEvent<Scene, FindEnemy> {
        protected override async ETTask Run(Scene scene, FindEnemy args) {
            GameRoom room = scene.GetComponent<Component_Rooms>().AddChild<GameRoom, GameRoomType>(GameRoomType.Match);
            await ETTask.CompletedTask;
        }
    }
}