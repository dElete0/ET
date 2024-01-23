using TrueSync;

namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOf(typeof (RoomServerComponent))]
    public class C2Room_ChangeGameSceneFinishHandler: MessageHandler<Scene, C2Room_ChangeGameSceneFinish>
    {
        protected override async ETTask Run(Scene root, C2Room_ChangeGameSceneFinish message)
        {
            Room room = root.GetComponent<Room>();
            RoomServerComponent roomServerComponent = room.GetComponent<RoomServerComponent>();
            RoomPlayer roomPlayer = room.GetComponent<RoomServerComponent>().GetChild<RoomPlayer>(message.PlayerId);
            roomPlayer.Progress = 100;
            
            if (!roomServerComponent.IsAllPlayerProgress100()) {
                return;
            }
            
            Log.Warning("所有玩家已进入房间");
            
            await room.Fiber.Root.GetComponent<TimerComponent>().WaitAsync(1000);

            Room2C_CGStart room2CStart = new() { StartTime = TimeInfo.Instance.ServerFrameTime() };
            foreach (RoomPlayer rp in roomServerComponent.Children.Values)
            {
                room2CStart.UnitInfo.Add(new CardGameUnitInfo()
                {
                    PlayerId = rp.Id,
                });
            }

            // 挂载房间的必要脚本
            room.AddComponent<CGServerUpdater, GameRoomType>(GameRoomType.Ai);
            room.AddComponent<ObjectWait>();
            GameEvent.Instance = GameEventFactory.Instance();
            
            //游戏开始
            room.Init(room2CStart.UnitInfo, room2CStart.StartTime);

            RoomMessageHelper.BroadCast(room, room2CStart);
        }
    }
}