namespace ET
{

    [ChildOf(typeof (RoomServerComponent))]
    public class RoomPlayer: Entity, IAwake<long> {
        
        public long PlayerId;
        public int Progress { get; set; }

        public bool IsOnline { get; set; } = true;
    }
}