using System.Collections.Generic;

namespace ET {

    public class GroupComponent {
        public Dictionary<long, GameGroup[]> PlayerGroups = new Dictionary<long, GameGroup[]>();
    }
    
    public class GameGroup {
        public string GroupName;
        public int HeroId;
        public int AgentId1;
        public int AgentId2;
        public List<int> Cards = new List<int>();
    }
}