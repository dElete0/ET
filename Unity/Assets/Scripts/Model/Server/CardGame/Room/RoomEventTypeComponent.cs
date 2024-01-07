using System.Collections.Generic;

namespace ET.Server {
    [ComponentOf(typeof(Room))]
    public class RoomEventTypeComponent : Entity, IAwake {
        //操作上限次数计数，玩家进行一次操作后归零，到达最大值后，只执行基础事件
        public int Count;
        
        public List<CardEventTypeComponent> CardEventTypeComponents = new List<CardEventTypeComponent>();
        public List<PlayerEventTypeComponent> PlayerEventTypeComponents = new List<PlayerEventTypeComponent>();
    }
}