using System;
using MemoryPack;

namespace ET
{
    [MemoryPackable]
    public partial struct CGInput
    {
        [MemoryPackOrder(0)]
        public TrueSync.TSVector2 V;

        [MemoryPackOrder(1)]
        public int Button;
        
        public bool Equals(CGInput other)
        {
            return this.V == other.V && this.Button == other.Button;
        }

        public override bool Equals(object obj)
        {
            return obj is CGInput other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.V, this.Button);
        }

        public static bool operator==(CGInput a, CGInput b)
        {
            if (a.V != b.V)
            {
                return false;
            }

            if (a.Button != b.Button)
            {
                return false;
            }

            return true;
        }

        public static bool operator !=(CGInput a, CGInput b)
        {
            return !(a == b);
        }
    }
}