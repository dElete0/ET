using System.Collections.Generic;

namespace ET {
    [ComponentOf(typeof(Component_Card))]
    public class Component_Player_Units : Entity, IAwake {
        public List<GameCard> units;
    }
}