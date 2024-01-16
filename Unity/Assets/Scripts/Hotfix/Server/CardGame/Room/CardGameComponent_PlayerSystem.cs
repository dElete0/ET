using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static partial class CardGameComponent_PlayerSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.CardGameComponent_Player self)
        {
            self.CostMax = 12;
        }

        public static CardColor GetColorByNum(this CardGameComponent_Player self, int num)
        {
            Room room = self.GetParent<Room>();
            CardGameComponent_Cards cards = room.GetComponent<CardGameComponent_Cards>();
            if (num < 2)
            {
                RoomCard hero = cards.GetChild<RoomCard>(self.Hero);
                return num == 0 ? hero.Colors.Item1 : hero.Colors.Item2;
            } else if (num < 4) {
                RoomCard agent1 = cards.GetChild<RoomCard>(self.Agent1);
                return num == 2 ? agent1.Colors.Item1 : agent1.Colors.Item2;
            } else {
                RoomCard agent2 = cards.GetChild<RoomCard>(self.Agent2);
                return num == 4 ? agent2.Colors.Item1 : agent2.Colors.Item2;
            }
        }

        public static List<long> GetAllUnits(this CardGameComponent_Player self) {
            List<long> units = new List<long>(self.Units);
            var enemyCards = self.GetParent<RoomPlayer>().GetEnemy().GetComponent<CardGameComponent_Player>();
            units.AddRange(enemyCards.Units);
            return units;
        }

        public static bool Contains(this long[] self, long id) {
            foreach (var cardId in self) {
                if (cardId == id)
                    return true;
            }

            return false;
        }
        
        public static void Remove(this long[] self, long id) {
            for(int i = 0; i < self.Length; i++) {
                if (self[i] == id) {
                    self[i] = 0;
                    return;
                }
            }
        }
        
        public static int Count(this long[] self) {
            int count = 0;
            for(int i = 0; i < self.Length; i++) {
                if (self[i] != 0) {
                    count++;
                }
            }
            return count;
        }

        public static bool TryAdd(this long[] self, long id) {
            for (int i = 0; i < self.Length; i++) {
                if (self[i] == 0) {
                    self[i] = id;
                    return true;
                }
            }
            return false;
        }
        
    }
}