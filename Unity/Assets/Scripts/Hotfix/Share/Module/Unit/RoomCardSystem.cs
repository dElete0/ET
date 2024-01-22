using System;
using System.Collections.Generic;
using ET.Server;

namespace ET {
    [EntitySystemOf(typeof(RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static partial class RoomCardSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RoomCard self, int configId, long playerId)
        {
            self.ConfigId = configId;
            self.PlayerId = playerId;
            CardEventTypeComponent cardEventTypeComponent =
                    self.AddComponent<CardEventTypeComponent, RoomEventTypeComponent>(
                        self.GetParent<Room>().GetComponent<RoomEventTypeComponent>());
            self.SetRoomCardByBase();
        }

        public static void SetCost(this ET.RoomCard self, int cost)
        {
            self.Cost = cost;
            self.CostD = cost;
        }

        public static void SetAttack(this ET.RoomCard self, int attack)
        {
            self.Attack = attack;
            self.AttackD = attack;
        }

        public static void SetHP(this ET.RoomCard self, int hp)
        {
            self.HP = hp;
            self.HPD = hp;
            self.HPMax = hp;
        }

        public static void SetColor(this ET.RoomCard self, int red, int blue, int white, int green, int black, int grey)
        {
            self.Red = red;
            self.RedD = red;
            self.Blue = blue;
            self.BlueD = blue;
            self.White = white;
            self.WhiteD = white;
            self.Green = green;
            self.GreenD = green;
            self.Black = black;
            self.BlackD = black;
            self.Grey = grey;
            self.GreyD = grey;
        }

        public static CardConfig Config(this RoomCard self)
        {
            return CardConfigCategory.Instance.Get(self.ConfigId);
        }

        public static CardType BaseType(this RoomCard self)
        {
            return (CardType)self.Config().Type;
        }

        public static void SetRoomCardByBase(this RoomCard self)
        {
            self.Name = self.Config().Name;
            self.SetCost(self.Config().Cost);
            self.SetAttack(self.Config().Attack);
            self.SetHP(self.Config().HP);
            self.SetColor(self.Config().Red, self.Config().Blue, self.Config().White, self.Config().Green, self.Config().Black, self.Config().Grey);
            self.UseCardType = (UseCardType)self.Config().Target;
            self.CardType = (CardType)self.Config().Type;
            if (self.CardType == CardType.Agent || self.CardType == CardType.Hero)
            {
                self.SetPlayerColors();
            }
            List<Power_Struct> basePowers = GetBasePowers(self);
            foreach (var power in basePowers)
            {
                if (power.TriggerPowerType == TriggerPowerType.Attribute)
                {
                    self.AttributePowers.Add(power.PowerType, 1);
                }
                else if (power.TriggerPowerType == TriggerPowerType.WhenTurnOverInHandCard)
                {
                    self.GetComponent<CardEventTypeComponent>().HanCardEventTypes.Add(TriggerEventFactory.TurnOver(), power.TriggerEvent);
                }
                else
                {
                    self.OtherPowers.Add(power);
                }
            }
        }

        private static void SetPlayerColors(this RoomCard self)
        {
            if (self.Red > 2)
            {
                self.Colors = (CardColor.Red, CardColor.Red);
            }
            else if (self.Blue > 2)
            {
                self.Colors = (CardColor.Blue, CardColor.Blue);
            }
            else if (self.Green > 2)
            {
                self.Colors = (CardColor.Green, CardColor.Green);
            }
            else if (self.Black > 2)
            {
                self.Colors = (CardColor.Black, CardColor.Black);
            }
            else if (self.White > 2)
            {
                self.Colors = (CardColor.White, CardColor.White);
            }
            else if (self.Grey > 2)
            {
                self.Colors = (CardColor.Grey, CardColor.Grey);
            }
            else
            {
                if (self.Red > 1)
                {
                    self.Colors.Item1 = CardColor.Red;
                }
                else if (self.Blue > 1)
                {
                    self.Colors.Item1 = CardColor.Blue;
                }
                else if (self.Green > 1)
                {
                    self.Colors.Item1 = CardColor.Green;
                }
                else if (self.Black > 1)
                {
                    self.Colors.Item1 = CardColor.Black;
                }
                else if (self.White > 1)
                {
                    self.Colors.Item1 = CardColor.White;
                }
                else if (self.Grey > 1)
                {
                    self.Colors.Item1 = CardColor.Grey;
                }

                if (self.Red == 1)
                {
                    self.Colors.Item2 = CardColor.Red;
                }
                else if (self.Blue == 1)
                {
                    self.Colors.Item2 = CardColor.Blue;
                }
                else if (self.Black == 1)
                {
                    self.Colors.Item2 = CardColor.Black;
                }
                else if (self.Green == 1)
                {
                    self.Colors.Item2 = CardColor.Green;
                }
                else if (self.Grey == 1)
                {
                    self.Colors.Item2 = CardColor.Grey;
                }
                else if (self.White == 1)
                {
                    self.Colors.Item2 = CardColor.White;
                }
            }
        }

        public static List<Power_Struct> GetArranges(this RoomCard self)
        {
            List<Power_Struct> powerStructs = new List<Power_Struct>();
            foreach (var power in self.OtherPowers)
            {
                if (power.TriggerPowerType == TriggerPowerType.Arrange)
                {
                    powerStructs.Add(power);
                }
            }
            return powerStructs;
        }

        public static List<Power_Struct> GetAura(this RoomCard self)
        {
            List<Power_Struct> powerStructs = new List<Power_Struct>();
            foreach (var power in self.OtherPowers)
            {
                if (power.TriggerPowerType == TriggerPowerType.Aura)
                {
                    powerStructs.Add(power);
                }
            }
            return powerStructs;
        }

        public static List<Power_Struct> GetRelease(this RoomCard self)
        {
            List<Power_Struct> powerStructs = new List<Power_Struct>();
            foreach (var power in self.OtherPowers)
            {
                if (power.TriggerPowerType == TriggerPowerType.Release)
                {
                    powerStructs.Add(power);
                }
            }

            return powerStructs;
        }

        public static RoomPlayer GetOwner(this RoomCard self)
        {
            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            RoomPlayer player = roomServerComponent.GetChild<RoomPlayer>(self.PlayerId);
            return player;
        }

        public static RoomCard GetHero(this RoomPlayer player)
        {
            CardGameComponent_Cards cards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            return cards.GetChild<RoomCard>(playerInfo.Hero);
        }

        public static List<Power_Struct> GetBasePowers(this RoomCard self)
        {
            List<Power_Struct> powerStructs = new List<Power_Struct>();
            if (self.Config().Effect1Type > 0)
            {
                powerStructs.Add(new Power_Struct()
                {
                    PowerType = (Power_Type)self.Config().Effect1Type,
                    TriggerPowerType = (TriggerPowerType)self.Config().Effect1From,
                    Count1 = self.Config().Effect1Num1,
                    Count2 = self.Config().Effect1Num2,
                    Count3 = self.Config().Effect1Num3,
                });
            }

            if (self.Config().Effect2Type > 0)
            {
                powerStructs.Add(new Power_Struct()
                {
                    PowerType = (Power_Type)self.Config().Effect2Type,
                    TriggerPowerType = (TriggerPowerType)self.Config().Effect2From,
                    Count1 = self.Config().Effect2Num1,
                    Count2 = self.Config().Effect2Num2,
                    Count3 = self.Config().Effect2Num3,
                });
            }

            if (self.Config().Effect3Type > 0)
            {
                powerStructs.Add(new Power_Struct()
                {
                    PowerType = (Power_Type)self.Config().Effect3Type,
                    TriggerPowerType = (TriggerPowerType)self.Config().Effect3From,
                    Count1 = self.Config().Effect3Num1,
                    Count2 = self.Config().Effect3Num2,
                    Count3 = self.Config().Effect3Num3,
                });
            }

            return powerStructs;
        }
    }
}