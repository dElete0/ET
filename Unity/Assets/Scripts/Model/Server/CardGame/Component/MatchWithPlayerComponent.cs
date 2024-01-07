using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class MatchWithPlayerComponent: Entity, IAwake
    {
        public List<long> waitMatchPlayers = new List<long>();
    }
}