using System;
using System.Collections.Generic;

namespace ET
{
    public interface ICGRollback
    {
    }
    
    public interface ICGRollbackSystem: ISystemType
    {
        void Run(Entity o);
    }
    
    public abstract class CGRollbackSystem<T> : ICGRollbackSystem where T: Entity, ICGRollback
    {
        void ICGRollbackSystem.Run(Entity o)
        {
            this.CGRollback((T)o);
        }

        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(ICGRollbackSystem);
        }

        int ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        protected abstract void CGRollback(T self);
    }
}