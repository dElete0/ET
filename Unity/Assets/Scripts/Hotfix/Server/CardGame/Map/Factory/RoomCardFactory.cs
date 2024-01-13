using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static partial class RoomCardFactory
    {
        public static RoomCard CreateGroupCard(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Unit;
            if (card.CardType == CardType.Star || card.CardType == CardType.Legend) {
                card.CardType = CardType.Unit;
            }
            return card;
        }

        public static RoomCard CreateHero(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Hero;

            //回合开始时，攻击计数清空
            card.GetComponent<CardEventTypeComponent>().UnitGameEventTypes.Add(TriggerEventFactory.TurnStart(), new GameEvent(GameEventType.TurnStart)
            {
                ToDo = (info) =>
                {
                    card.AttackCount = card.AttackCountMax;
                    card.IsCallThisTurn = false;
                }
            });
            return card;
        }

        public static RoomCard CreateAgent(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Agent;
            card.UseCardType = UseCardType.NoTarget;
            
            //回合开始时，攻击计数清空
            card.GetComponent<CardEventTypeComponent>().UnitGameEventTypes.Add(TriggerEventFactory.TurnStart(), new GameEvent(GameEventType.TurnStart)
            {
                ToDo = (info) =>
                {
                    card.AttackCount = card.AttackCountMax;
                    card.IsCallThisTurn = false;
                }
            });
            return card;
        }
    }
}