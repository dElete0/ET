namespace ET {
    [ComponentOf(typeof(GameCard))]
    public class Component_Card_Agent : Entity, IAwake<CardPos> {
        public CardPos CardPos;//所处位置
    }
}