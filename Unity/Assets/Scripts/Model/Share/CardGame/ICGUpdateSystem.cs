using System;

namespace ET
{
    public interface ICGUpdate
    {
    }
    
    public interface ICGUpdateSystem: ISystemType
    {
        void Run(CGEntity o);
    }
    
    [CGEntitySystem]
    public abstract class CGUpdateSystem<T> : ICGUpdateSystem where T: CGEntity, ICGUpdate
    {
        void ICGUpdateSystem.Run(CGEntity o)
        {
            this.CGUpdate((T)o);
        }

        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(ICGUpdateSystem);
        }

        int ISystemType.GetInstanceQueueIndex()
        {
            return CGQueneUpdateIndex.CGUpdate;
        }

        protected abstract void CGUpdate(T self);
    }
}