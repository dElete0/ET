using System.Collections.Generic;

namespace ET {
    [ComponentOf(typeof(Component_Card))]
    public class Component_Player_Group : Entity, IAwake {
        public List<GameCard> cards;
    }
}