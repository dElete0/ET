using System.Collections.Generic;

namespace ET {
    [ComponentOf(typeof(Room))]
    public class RoomServerComponent : Entity, IAwake<List<long>> {
    
    }
}