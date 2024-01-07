using System.Collections.Generic;

namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    public class RoomManager2Room_CGInitHandler: MessageHandler<Scene, RoomManager2Room_CGInit, Room2RoomManager_CGInit>
    {
        protected override async ETTask Run(Scene root, RoomManager2Room_CGInit request, Room2RoomManager_CGInit response)
        {
            Room room = root.AddComponent<Room>();
            room.Name = "Server";
            room.AddComponent<RoomServerComponent, List<long>>(request.PlayerIds);

            room.CGWorld = new CGWorld(SceneType.LockStepServer);
            await ETTask.CompletedTask;
        }
    }
}