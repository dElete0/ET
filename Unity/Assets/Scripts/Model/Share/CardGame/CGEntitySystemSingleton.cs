using System;
using System.Collections.Generic;

namespace ET
{
    [UniqueId(-1, 1)]
    public static class CGQueneUpdateIndex
    {
        public const int None = -1;
        public const int CGUpdate = 0;
        public const int Max = 1;
    }
    
    [Code]
    public class CGEntitySystemSingleton: Singleton<CGEntitySystemSingleton>, ISingletonAwake
    {
        private TypeSystems TypeSystems { get; set; }
        
        private readonly DoubleMap<Type, long> cgEntityTypeLongHashCode = new();
        
        public void Awake()
        {
            this.TypeSystems = new(CGQueneUpdateIndex.Max);
            foreach (Type type in CodeTypes.Instance.GetTypes(typeof (CGEntitySystemAttribute)))
            {
                object obj = Activator.CreateInstance(type);

                if (obj is not ISystemType iSystemType)
                {
                    continue;
                }

                TypeSystems.OneTypeSystems oneTypeSystems = this.TypeSystems.GetOrCreateOneTypeSystems(iSystemType.Type());
                oneTypeSystems.Map.Add(iSystemType.SystemType(), obj);
                int index = iSystemType.GetInstanceQueueIndex();
                if (index > CGQueneUpdateIndex.None && index < CGQueneUpdateIndex.Max)
                {
                    oneTypeSystems.QueueFlag[index] = true;
                }
            }
            
            foreach (var kv in CodeTypes.Instance.GetTypes())
            {
                Type type = kv.Value;
                if (typeof(CGEntity).IsAssignableFrom(type))
                {
                    long hash = type.FullName.GetLongHashCode();
                    try
                    {
                        this.cgEntityTypeLongHashCode.Add(type, type.FullName.GetLongHashCode());
                    }
                    catch (Exception e)
                    {
                        Type sameHashType = this.cgEntityTypeLongHashCode.GetKeyByValue(hash);
                        throw new Exception($"long hash add fail: {type.FullName} {sameHashType.FullName}", e);
                    }
                }
            }
        }
        
        public long GetLongHashCode(Type type)
        {
            return this.cgEntityTypeLongHashCode.GetValueByKey(type);
        }
        
        public TypeSystems.OneTypeSystems GetOneTypeSystems(Type type)
        {
            return this.TypeSystems.GetOneTypeSystems(type);
        }
        
        public void CGRollback(Entity entity)
        {
            if (entity is not ICGRollback)
            {
                return;
            }
            
            List<object> iCGRollbackSystems = this.TypeSystems.GetSystems(entity.GetType(), typeof (ICGRollbackSystem));
            if (iCGRollbackSystems == null)
            {
                return;
            }

            foreach (ICGRollbackSystem iCGRollbackSystem in iCGRollbackSystems)
            {
                if (iCGRollbackSystem == null)
                {
                    continue;
                }

                try
                {
                    iCGRollbackSystem.Run(entity);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }

        public void CGUpdate(CGEntity entity)
        {
            if (entity is not ICGUpdate)
            {
                return;
            }
            
            List<object> iCGUpdateSystems = TypeSystems.GetSystems(entity.GetType(), typeof (ICGUpdateSystem));
            if (iCGUpdateSystems == null)
            {
                return;
            }

            foreach (ICGUpdateSystem iCGUpdateSystem in iCGUpdateSystems)
            {
                try
                {
                    iCGUpdateSystem.Run(entity);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}