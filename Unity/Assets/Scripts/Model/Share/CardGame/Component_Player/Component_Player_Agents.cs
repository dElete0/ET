namespace ET {
    [ComponentOf(typeof(Component_Card))]
    public class Component_Player_Agents : Entity, IAwake {
        public EntityRef<GameCard> Agent1, Agent2;
    }
}