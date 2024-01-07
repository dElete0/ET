using System;
using MemoryPack;
using MongoDB.Bson.Serialization.Attributes;
using TrueSync;

namespace ET
{
    [ChildOf(typeof(CGUnitComponent))]
    [MemoryPackable]
    public partial class CGUnit: CGEntity, IAwake, ISerializeToEntity
    {
        public TSVector Position
        {
            get;
            set;
        }
    }
}