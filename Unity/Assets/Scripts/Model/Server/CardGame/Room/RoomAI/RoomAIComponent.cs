using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(RoomPlayer))]
    public class RoomAIComponent : Entity, IAwake, IUpdate {
        public bool IsToDo;
    }
}