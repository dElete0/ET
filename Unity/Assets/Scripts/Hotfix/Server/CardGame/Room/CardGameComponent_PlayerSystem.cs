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
            self.CostMax = CardGameMsg.CostMax;
            self.HandCardsCountMax = CardGameMsg.HandCardsCountMax;
        }

        public static (CardColor, RoomCard) GetColorByNum(this CardGameComponent_Player self, int num)
        {
            Room room = self.GetParent<Room>();
            CardGameComponent_Cards cards = room.GetComponent<CardGameComponent_Cards>();
            if (num < 2)
            {
                RoomCard hero = cards.GetChild<RoomCard>(self.Hero);
                return (num == 0 ? hero.Colors.Item1 : hero.Colors.Item2, hero);
            } else if (num < 4) {
                RoomCard agent1 = cards.GetChild<RoomCard>(self.Agent1);
                return (num == 2 ? agent1.Colors.Item1 : agent1.Colors.Item2, agent1);
            } else {
                RoomCard agent2 = cards.GetChild<RoomCard>(self.Agent2);
                return (num == 4 ? agent2.Colors.Item1 : agent2.Colors.Item2, agent2);
            }
        }

        public static List<long> GetAllUnits(this CardGameComponent_Player self) {
            List<long> units = new List<long>(self.Units);
            var enemyCards = self.GetParent<RoomPlayer>().GetEnemy().GetComponent<CardGameComponent_Player>();
            units.AddRange(enemyCards.Units);
            return units;
        }
        
        public static List<long> GetAllActors(this CardGameComponent_Player self) {
            List<long> units = new List<long>(self.Units);
            var enemyCards = self.GetParent<RoomPlayer>().GetEnemy().GetComponent<CardGameComponent_Player>();
            units.AddRange(enemyCards.Units);
            units.Add(self.Hero);
            units.Add(enemyCards.Hero);
            if (self.Agent1 > 0) units.Add(self.Agent1);
            if (self.Agent2 > 0) units.Add(self.Agent2);
            if (enemyCards.Agent1 > 0) units.Add(enemyCards.Agent1);
            if (enemyCards.Agent2 > 0) units.Add(enemyCards.Agent2);
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

        public static List<RoomCard> GetHandCards(this CardGameComponent_Player playerInfo, CardGameComponent_Cards cards) {
            List<RoomCard> handCards = new List<RoomCard>();
            foreach (var unitId in playerInfo.HandCards) {
                handCards.Add(cards.GetChild<RoomCard>(unitId));
            }
            return handCards;
        }

        public static List<RoomCard> GetUnits(this CardGameComponent_Player playerInfo, CardGameComponent_Cards cards) {
            List<RoomCard> units = new List<RoomCard>();
            foreach (var unitId in playerInfo.Units) {
                units.Add(cards.GetChild<RoomCard>(unitId));
            }
            return units;
        }

        public static List<RoomCard> GetUnitsByPower(this List<RoomCard> cards, Power_Type power) {
            List<RoomCard> returnCards = new List<RoomCard>();
            foreach (var card in cards) {
                if (card.AttributePowers.ContainsKey(power)) {
                    returnCards.Add(card);
                }
            }
            return returnCards;
        }

        public static int GetHurtByAttack(this List<RoomCard> cards) {
            int hurt = 0;
            foreach (var card in cards) {
                if (card.UnitType != CardUnitType.ExclusionZone) {
                    hurt += card.Attack;
                }
            }
            return hurt;
        }
        
        public static (int, List<RoomCard>) GetHurtByHandCards(this List<RoomCard> cards, int cost) {
            int allHurt = 0;
            List<RoomCard> returnCards = new List<RoomCard>();
            foreach (var card in cards) {
                int hurt = card.GetHurtByHandCard();
                if (hurt > 0) {
                    allHurt += hurt;
                    returnCards.Add(card);
                }
            }
            return (allHurt, returnCards);
        }

        public static int GetHurtByHandCard(this RoomCard card) {
            int hurt = 0;
            List<Power_Struct> powers = null;
            if (card.CardType == CardType.Unit) {
                if (card.AttributePowers.ContainsKey(Power_Type.Charge)) {
                    hurt += card.Attack;
                    if (card.AttributePowers.ContainsKey(Power_Type.AttackTwice)) {
                        hurt += card.Attack;
                    }
                }
                powers = card.GetArranges();
            } else if (card.CardType == CardType.Plot || card.CardType == CardType.Magic) {
                powers = card.GetRelease();
            }

            if (powers != null && powers.Count > 1) {
                foreach (var power in powers) {
                    switch (power.PowerType) {
                        case Power_Type.DamageHurt:
                        case Power_Type.DamageAllActor:
                        case Power_Type.DamageEnemyHero:
                            hurt += power.Count1;
                            break;
                    }
                }
            }

            return hurt;
        }
    }
}