using System;

namespace ET
{
    [EnableMethod]
    public abstract partial class CGEntity: Entity
    {
        public new K AddComponent<K>(bool isFromPool = false) where K : CGEntity, IAwake, new()
        {
            return this.AddComponentWithId<K>(this.GetId(), isFromPool);
        }

        public new K AddComponent<K, P1>(P1 p1, bool isFromPool = false) where K : CGEntity, IAwake<P1>, new()
        {
            return this.AddComponentWithId<K, P1>(this.GetId(), p1, isFromPool);
        }

        public new K AddComponent<K, P1, P2>(P1 p1, P2 p2, bool isFromPool = false) where K : CGEntity, IAwake<P1, P2>, new()
        {
            return this.AddComponentWithId<K, P1, P2>(this.GetId(), p1, p2, isFromPool);
        }

        public new K AddComponent<K, P1, P2, P3>(P1 p1, P2 p2, P3 p3, bool isFromPool = false) where K : CGEntity, IAwake<P1, P2, P3>, new()
        {
            return this.AddComponentWithId<K, P1, P2, P3>(this.GetId(), p1, p2, p3, isFromPool);
        }

        [EnableAccessEntiyChild]
        public new T AddChild<T>(bool isFromPool = false) where T : CGEntity, IAwake
        {
            return this.AddChildWithId<T>(this.GetId(), isFromPool);
        }

        [EnableAccessEntiyChild]
        public new T AddChild<T, A>(A a, bool isFromPool = false) where T : CGEntity, IAwake<A>
        {
            return this.AddChildWithId<T, A>(this.GetId(), a, isFromPool);
        }

        [EnableAccessEntiyChild]
        public new T AddChild<T, A, B>(A a, B b, bool isFromPool = false) where T : CGEntity, IAwake<A, B>
        {
            return this.AddChildWithId<T, A, B>(this.GetId(), a, b, isFromPool);
        }

        [EnableAccessEntiyChild]
        public new T AddChild<T, A, B, C>(A a, B b, C c, bool isFromPool = false) where T : CGEntity, IAwake<A, B, C>
        {
            return this.AddChildWithId<T, A, B, C>(this.GetId(), a, b, c, isFromPool);
        }

        protected override long GetLongHashCode(Type type)
        {
            return CGEntitySystemSingleton.Instance.GetLongHashCode(type);
        }

        protected override void RegisterSystem()
        {
            CGWorld cgWorld = (CGWorld)this.IScene;
            TypeSystems.OneTypeSystems oneTypeSystems = CGEntitySystemSingleton.Instance.GetOneTypeSystems(this.GetType());
            if (oneTypeSystems == null)
            {
                return;
            }

            if (oneTypeSystems.QueueFlag[CGQueneUpdateIndex.CGUpdate])
            {
                cgWorld.RegisterSystem(this);
            }
        }
    }
}