using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(RoomPlayer))]
    public class CardGameComponent_Player : Entity, IAwake {
        //基础值
        public long Hero;
        public long Agent1, Agent2;
        public int NowColor;
        
        public List<long> HandCards = new List<long>();
        public List<long> Groups = new List<long>();
        public List<long> Units = new List<long>();
        
        public int HandCardsCountMax;
        public int GroupCountMax;
        public int UnitCountMax;
        public const int HandCardsCountMaxD = 10;
        public const int GroupCountMaxD = 50;
        public const int UnitCountMaxD = 8;

        public int Cost;
        public int CostTotal;
        public int CostMax;
        public const int CostMaxD = 12;

        public int Red;
        public int Green;
        public int Blue;
        public int Black;
        public int White;
        public int Grey;

        //本场召唤的红龙数量
        public int RedGragonNum;
        //本场的传承计数
        public Dictionary<int, int> InheritCount = new Dictionary<int, int>();
        //本场打出过的法术
        public List<int> UsedMagicList = new List<int>();
    }
}