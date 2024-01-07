using System.Collections.Generic;

namespace ET.Server {
    [ComponentOf(typeof(RoomPlayer))]
    public partial class PlayerEventTypeComponent : Entity, IAwake<RoomEventTypeComponent>, IDestroy {
        public RoomEventTypeComponent RoomEvent;
        //监听器及触发事件
        public Dictionary<TriggerEvent, GameEvent> WaitGameEventTypes = new Dictionary<TriggerEvent, GameEvent>();
    }
}