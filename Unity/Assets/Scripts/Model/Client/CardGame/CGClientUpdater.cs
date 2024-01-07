using TrueSync;

namespace ET.Client
{
    [ComponentOf(typeof(Room))]
    public class CGClientUpdater: Entity, IAwake, IUpdate
    {
        public LSInput Input = new();
        
        public long MyId { get; set; }
    }
}