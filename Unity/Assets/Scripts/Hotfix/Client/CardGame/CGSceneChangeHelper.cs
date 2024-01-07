namespace ET.Client
{

    public static partial class CGSceneChangeHelper
    {
        // 场景切换协程
        public static async ETTask SceneChangeTo(Scene root, string sceneName, long sceneInstanceId)
        {
            root.RemoveComponent<Room>();

            Room room = root.AddComponentWithId<Room>(sceneInstanceId);
            room.Name = sceneName;

            // 等待表现层订阅的事件完成
            await EventSystem.Instance.PublishAsync(root, new CGSceneChangeStart() {Room = room});

            root.GetComponent<ClientSenderCompnent>().Send(new C2Room_ChangeGameSceneFinish());
            
            // 等待Room2C_EnterMap消息
            WaitType.Wait_Room2C_GameStart waitRoom2CStart = await root.GetComponent<ObjectWait>().Wait<WaitType.Wait_Room2C_GameStart>();

            room.CGWorld = new CGWorld(SceneType.LockStepClient);
            room.Init(waitRoom2CStart.Message.UnitInfo, waitRoom2CStart.Message.StartTime);
            
            room.AddComponent<CGClientUpdater>();

            // 这个事件中可以订阅取消loading
            EventSystem.Instance.Publish(root, new CGSceneInitFinish());
        }
        
        // 场景切换协程
        public static async ETTask SceneChangeToReplay(Scene root, Replay replay)
        {
            root.RemoveComponent<Room>();

            Room room = root.AddComponent<Room>();
            room.Name = "Map1";
            room.IsReplay = true;
            room.Replay = replay;
            room.CGWorld = new CGWorld(SceneType.LockStepClient);
            room.Init(replay.UnitInfos, TimeInfo.Instance.ServerFrameTime());
            
            // 等待表现层订阅的事件完成
            await EventSystem.Instance.PublishAsync(root, new CGSceneChangeStart() {Room = room});
            

            //room.AddComponent<CGReplayUpdater>();
            // 这个事件中可以订阅取消loading
            EventSystem.Instance.Publish(root, new CGSceneInitFinish());
        }
        
        // 场景切换协程
        public static async ETTask SceneChangeToReconnect(Scene root, G2C_Reconnect message)
        {
            root.RemoveComponent<Room>();

            Room room = root.AddComponent<Room>();
            room.Name = "Map1";
            
            room.CGWorld = new CGWorld(SceneType.LockStepClient);
            room.Init(message.UnitInfos, message.StartTime, message.Frame);
            
            // 等待表现层订阅的事件完成
            await EventSystem.Instance.PublishAsync(root, new CGSceneChangeStart() {Room = room});


            room.AddComponent<CGClientUpdater>();
            // 这个事件中可以订阅取消loading
            EventSystem.Instance.Publish(root, new CGSceneInitFinish());
        }
    }
}