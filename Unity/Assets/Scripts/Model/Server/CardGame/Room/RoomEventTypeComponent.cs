using System.Collections.Generic;

namespace ET.Server {
    [ComponentOf(typeof(Room))]
    public class RoomEventTypeComponent : Entity, IAwake {
        //操作上限次数计数，玩家进行一次操作后归零，到达最大值后，只执行基础事件
        public int Count;
        
        public List<CardEventTypeComponent> CardEventTypeComponents = new List<CardEventTypeComponent>();
        public List<PlayerEventTypeComponent> PlayerEventTypeComponents = new List<PlayerEventTypeComponent>();
    }

    //事件标记系统，每发起一次事件创建一次，记录死亡标记等最后执行的事件
    public class EventInfo {
        public int Count;
        //死亡标记
        public List<RoomCard> DeadList = new List<RoomCard>();
    }
}