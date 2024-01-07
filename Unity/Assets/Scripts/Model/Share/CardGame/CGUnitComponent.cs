using MemoryPack;

namespace ET
{
    [ComponentOf(typeof(CGWorld))]
    [MemoryPackable]
    public partial class CGUnitComponent: CGEntity, IAwake, ISerializeToEntity
    {
    }
}