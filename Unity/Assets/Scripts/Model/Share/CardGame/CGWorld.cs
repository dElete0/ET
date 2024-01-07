using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using MemoryPack;
using TrueSync;

namespace ET
{
    public static class CGWorldSystem
    {
        public static CGWorld CGWorld(this CGEntity entity)
        {
            return entity.IScene as CGWorld;
        }

        public static long GetId(this CGEntity entity)
        {
            return entity.CGWorld().GetId();
        }
        
        public static TSRandom GetRandom(this CGEntity entity)
        {
            return entity.CGWorld().Random;
        }
    }

    [EnableMethod]
    [ChildOf]
    [ComponentOf]
    [MemoryPackable]
    public partial class CGWorld: Entity, IAwake, IScene
    {
        [MemoryPackConstructor]
        public CGWorld()
        {
        }
        
        public CGWorld(SceneType sceneType)
        {
            this.Id = this.GetId();

            this.SceneType = sceneType;
        }

        private readonly CGUpdater updater = new();
        
        [BsonIgnore]
        [MemoryPackIgnore]
        public Fiber Fiber { get; set; }
        
        [BsonElement]
        [MemoryPackInclude]
        private long idGenerator;

        public long GetId()
        {
            return ++this.idGenerator;
        }

        public TSRandom Random { get; set; }
        
        [BsonIgnore]
        [MemoryPackIgnore]
        public SceneType SceneType { get; set; }
        
        public int Frame { get; set; }

        public void Update()
        {
            this.updater.Update();
            ++this.Frame;
        }

        public void RegisterSystem(CGEntity entity)
        {
            this.updater.Add(entity);
        }
        
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

        public new T AddChild<T>(bool isFromPool = false) where T : CGEntity, IAwake
        {
            return this.AddChildWithId<T>(this.GetId(), isFromPool);
        }

        public new T AddChild<T, A>(A a, bool isFromPool = false) where T : CGEntity, IAwake<A>
        {
            return this.AddChildWithId<T, A>(this.GetId(), a, isFromPool);
        }

        public new T AddChild<T, A, B>(A a, B b, bool isFromPool = false) where T : CGEntity, IAwake<A, B>
        {
            return this.AddChildWithId<T, A, B>(this.GetId(), a, b, isFromPool);
        }

        public new T AddChild<T, A, B, C>(A a, B b, C c, bool isFromPool = false) where T : CGEntity, IAwake<A, B, C>
        {
            return this.AddChildWithId<T, A, B, C>(this.GetId(), a, b, c, isFromPool);
        }
        
        protected override long GetLongHashCode(Type type)
        {
            return CGEntitySystemSingleton.Instance.GetLongHashCode(type);
        }
    }
}