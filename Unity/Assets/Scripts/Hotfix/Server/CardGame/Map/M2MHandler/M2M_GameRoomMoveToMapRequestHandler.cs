using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    public class M2M_GameRoomMoveToMapRequestHandler : MessageHandler<Scene, M2M_GameRoomMoveToMapRequest, M2M_GameRoomMoveToMapResponse>
    {
        protected override async ETTask Run(Scene scene, M2M_GameRoomMoveToMapRequest request, M2M_GameRoomMoveToMapResponse response)
        {
            //Log.Warning("Sever内部受到指令");
            Component_Rooms rooms = scene.GetComponent<Component_Rooms>();
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            GameRoom room = MongoHelper.Deserialize<GameRoom>(request.GameRoom);

            rooms.AddChild(room);

            foreach (byte[] bytes in request.Entitys)
            {
                Entity entity = MongoHelper.Deserialize<Entity>(bytes);
                room.AddComponent(entity);
            }

            room.AddComponent<ObjectWait>();

            //Log.Warning("重新创建GamePlayer");
            if (room.type == GameRoomType.Ai) {
                room.AddComponent<Component_Room_GamePlayer, int>(2);
                GamePlayer aiPlayer = GamePlayerFactory.CreatePlayer(room, GamePlayerType.ai);
                GamePlayer gamePlayer = GamePlayerFactory.CreatePlayer(room, GamePlayerType.man);
                
                aiPlayer.unit = MongoHelper.Deserialize<Unit>(request.Units[0]);
                gamePlayer.unit = MongoHelper.Deserialize<Unit>(request.Units[1]);

                gamePlayer.unit.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.OrderedMessage);

                unitComponent.AddChild(aiPlayer.unit);
                unitComponent.Add(aiPlayer.unit);
                unitComponent.AddChild(gamePlayer.unit);
                unitComponent.Add(gamePlayer.unit);
                
                SetPlayerUnit(scene, gamePlayer);
            }
            
            // 通知客户端创建GameUI
            M2C_CreateRoom m2CCreateRoom = new() { };
            foreach (var v in room.GetComponent<Component_Room_GamePlayer>().Children)
            {
                Log.Warning("通知客户端创建GameUI");
                GamePlayer player = v.Value as GamePlayer;
                MapMessageHelper.SendToClient(player.unit, m2CCreateRoom);

                Log.Warning("通知客户端创建My Unit");
                M2C_CreateMyUnit m2CCreateUnits = new();
                m2CCreateUnits.Unit = UnitHelper.CreateUnitInfo(player.unit);
                MapMessageHelper.SendToClient(player.unit, m2CCreateUnits);
            }

            // 解锁location，可以接收发给Unit的消息
            // await scene.Root().GetComponent<LocationProxyComponent>().UnLock(LocationType.Unit, unit.Id, request.OldActorId, unit.GetActorId());
            await EventSystem.Instance.PublishAsync(scene, new GameEventType_GameBuildInMapScene() { GameRoom = room});
            await ETTask.CompletedTask;
        }

        //创建Unit
        private static void SetPlayerUnit(Scene scene, GamePlayer player)
        {
            Unit unit = UnitFactory.Create(scene, player.Id, player.IsBot ? UnitType.Monster : UnitType.Player);
            unit.AddComponent<NumericComponent>();
            Log.Debug("建立Actor机制,并连接：Unit:" + unit.Id);
            player.unit = unit;
        }
    }
}