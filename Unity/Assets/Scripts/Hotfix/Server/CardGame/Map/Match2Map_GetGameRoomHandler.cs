using System;
using System.Collections.Generic;

namespace ET.Server
{
    [MessageHandler(SceneType.Map)]
    public class Map2Match_GetGameRoomHandler : MessageHandler<Scene, Match2Map_GetGameRoom, Map2Match_GetGameRoom>
    {
        protected override async ETTask Run(Scene root, Match2Map_GetGameRoom request, Map2Match_GetGameRoom response)
        {
            Fiber fiber = root.Fiber();
            int fiberId = await FiberManager.Instance.Create(SchedulerType.ThreadPool, fiber.Zone, SceneType.RoomRoot, "RoomRoot");
            ActorId roomRootActorId = new(fiber.Process, fiberId);

            // 发送消息给房间纤程，初始化
            RoomManager2Room_CGInit roomManager2RoomInit = RoomManager2Room_CGInit.Create();
            roomManager2RoomInit.PlayerIds.AddRange(request.PlayerIds);
            await root.GetComponent<MessageSender>().Call(roomRootActorId, roomManager2RoomInit);
			
            response.ActorId = roomRootActorId;
            await ETTask.CompletedTask;
        }
    }
}