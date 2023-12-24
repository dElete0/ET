using Unity.Mathematics;

namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    public class M2M_CreateUnitInRoomRequestHandler: MessageHandler<Scene, M2M_CreateUnitInRoomRequest, M2M_CreateUnitInRoomResponse>
    {
        protected override async ETTask Run(Scene scene, M2M_CreateUnitInRoomRequest request, M2M_CreateUnitInRoomResponse response)
        {
            Log.Warning("Sever内部受到指令");
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            Unit unit = MongoHelper.Deserialize<Unit>(request.Unit);

            unitComponent.AddChild(unit);
            unitComponent.Add(unit);

            Log.Warning("为GamePlayer.unit重新赋值");
            GamePlayer player = scene.GetComponent<Component_Rooms>().GetChild<GameRoom>(request.RoomId).GetComponent<Component_Room_GamePlayer>()
                    .GetChild<GamePlayer>(request.PlayerId);
            player.unit = unit;

            foreach (byte[] bytes in request.Entitys)
            {
                Entity entity = MongoHelper.Deserialize<Entity>(bytes);
                unit.AddComponent(entity);
            }

            //unit.AddComponent<MoveComponent>();
            //unit.AddComponent<PathfindingComponent, string>(scene.Name);
            unit.Position = new float3(-10, 0, -10);

            unit.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.OrderedMessage);

            // 通知客户端创建GameUI
            M2C_CreateRoom m2CCreateRoom = new() { SceneInstanceId = scene.InstanceId, SceneName = scene.Name };
            MapMessageHelper.SendToClient(unit, m2CCreateRoom);
            //M2C_StartSceneChange m2CStartSceneChange = new() { SceneInstanceId = scene.InstanceId, SceneName = scene.Name };
            //MapMessageHelper.SendToClient(unit, m2CStartSceneChange);

            // 通知客户端创建My Unit
            M2C_CreateMyUnit m2CCreateUnits = new();
            m2CCreateUnits.Unit = UnitHelper.CreateUnitInfo(unit);
            MapMessageHelper.SendToClient(unit, m2CCreateUnits);

            // 解锁location，可以接收发给Unit的消息
            await scene.Root().GetComponent<LocationProxyComponent>().UnLock(LocationType.Unit, unit.Id, request.OldActorId, unit.GetActorId());
        }
    }
}