using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static partial class RoomCardFactory
    {
        public static RoomCard CreateGroupCard(CardGameComponent_Cards cards, int configId, long playerId)
        {
            RoomCard card = cards.AddChild<RoomCard, int, long>(configId, playerId);
            if (card.CardType == CardType.Star || 
                card.CardType == CardType.Legend || 
                card.CardType == CardType.Unit ||
                card.CardType == CardType.ExclusionZone) {
                card.UnitType = (CardUnitType)card.CardType;
                card.CardType = CardType.Unit;
            }
            return card;
        }

        public static RoomCard CreateHero(CardGameComponent_Cards cards, int configId, long playerId)
        {
            RoomCard card = cards.AddChild<RoomCard, int, long>(configId, playerId);
            card.CardType = CardType.Hero;

            return card;
        }

        public static RoomCard CreateAgent(CardGameComponent_Cards cards, int configId, long playerId)
        {
            RoomCard card = cards.AddChild<RoomCard, int, long>(configId, playerId);
            card.CardType = CardType.Agent;
            card.UseCardType = UseCardType.NoTarget;
            return card;
        }
    }
}