namespace ET {
    
    [ComponentOf(typeof(GameCard))]
    public class Component_Card_Hero : Entity, IAwake {
        public CardColor color1, color2;
    }
}
