using MemoryPack;

namespace ET
{
    [ComponentOf(typeof(CGUnit))]
    [MemoryPackable]
    public partial class CGInputComponent: CGEntity, ICGUpdate, IAwake, ISerializeToEntity
    {
        public CGInput CGInput { get; set; }
    }
}