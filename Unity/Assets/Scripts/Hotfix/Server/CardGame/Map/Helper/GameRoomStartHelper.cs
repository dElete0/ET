namespace ET.Server {
    public static partial class GameRoomStartHelper {
        public static async ETTask GameRoomStart(long roomId, long playerId, Unit unit, ActorId sceneInstanceId, string sceneName)
        {
            {
                Scene root = unit.Root();

                // location加锁
                long unitId = unit.Id;

                M2M_CreateUnitInRoomRequest request = M2M_CreateUnitInRoomRequest.Create();
                request.OldActorId = unit.GetActorId();
                request.Unit = unit.ToBson();
                request.RoomId = roomId;
                request.PlayerId = playerId;
                foreach (Entity entity in unit.Components.Values) {
                    if (entity is ITransfer) {
                        request.Entitys.Add(entity.ToBson());
                    }
                }
                unit.Dispose();
                
                Log.Warning("向客户端发送在房间中创建Unit指令");
                await root.GetComponent<LocationProxyComponent>().Lock(LocationType.Unit, unitId, request.OldActorId);
                await root.GetComponent<MessageSender>().Call(sceneInstanceId, request);
                
                //room.GetComponent<ObjectWait>().Notify(new Wait_GameEventType_UnitRebuildFinish());
            }
        }
    }
}