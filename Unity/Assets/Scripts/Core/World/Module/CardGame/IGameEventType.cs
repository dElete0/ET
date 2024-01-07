using System;

namespace ET {

    public interface IGameEventType {
        //触发条件
        public Func<bool> Triggeer {
            get;
            set;
        }
        //触发一次就失效
        public bool OnceEvent {
            set;
            get;
        }
        //执行事件
        public Action ToDo {
            set;
            get;
        }
    }
	
    public abstract class GameEvent<S, A>: IGameEventType where S: class, IScene where A: struct
    {
        public Type Type
        {
            get
            {
                return typeof (A);
            }
        }

        protected abstract ETTask Run(S scene, A a);

        public async ETTask Handle(S scene, A a)
        {
            try
            {
                await Run(scene, a);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public Func<bool> Triggeer { get; set; }
        public bool OnceEvent { get; set; }
        public Action ToDo { get; set; }
    }
}