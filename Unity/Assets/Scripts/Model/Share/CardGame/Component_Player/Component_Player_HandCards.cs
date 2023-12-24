using System.Collections.Generic;

namespace ET {
    [ComponentOf(typeof(Component_Card))]
    public class Component_Player_HandCards : Entity, IAwake {
        public int CountMax;
        public List<GameCard> HandCards;
    }
}