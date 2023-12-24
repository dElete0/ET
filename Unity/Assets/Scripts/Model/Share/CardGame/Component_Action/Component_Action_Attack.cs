namespace ET {
    [ComponentOf(typeof(GameAction))]
    public class Component_Action_Attack : Entity, IAwake {
        public long actor;
        public long target;
    }
}