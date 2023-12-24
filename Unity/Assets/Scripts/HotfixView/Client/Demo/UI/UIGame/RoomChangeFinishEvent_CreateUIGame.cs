namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class RoomChangeFinishEvent_CreateUIGame : AEvent<Scene, RoomChangeFinish>
    {
        protected override async ETTask Run(Scene scene, RoomChangeFinish args)
        {
            //收到请求后，移除Main,进入GameUI
            await UIHelper.Remove(scene, UIType.UIMain);
            //进入游戏场景
            await UIHelper.Create(scene, UIType.UIGame, UILayer.Mid);
            GameRoom room = scene.GetComponent<Component_Rooms>().AddChild<GameRoom, GameRoomType>(GameRoomType.Ai);
        }
    }
}