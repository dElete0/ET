using System.Collections.Generic;

namespace ET.Server {
    [ComponentOf(typeof(RoomCard))]
    public partial class CardEventTypeComponent : Entity, IAwake<RoomEventTypeComponent>, IDestroy {
        public RoomEventTypeComponent RoomEvent;
        //监听器及触发事件(任意位置)
        public Dictionary<TriggerEvent, GameEvent> AllGameEventTypes = new Dictionary<TriggerEvent, GameEvent>();
        //监听器（在场上才触发,被沉默会被清空）
        public Dictionary<TriggerEvent, GameEvent> UnitGameEventTypes = new Dictionary<TriggerEvent, GameEvent>();
        //监听器（在手上才触发）
    }
}