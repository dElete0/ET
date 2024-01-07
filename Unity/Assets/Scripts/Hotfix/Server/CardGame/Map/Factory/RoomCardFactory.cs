using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static partial class RoomCardFactory
    {
        public static RoomCard CreateUnitCard(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Unit;
            return card;
        }

        public static RoomCard CreateMagic(CardGameComponent_Cards cards, int configId) {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Magic;
            return card;
        }

        public static RoomCard CreateHero(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Hero;
            return card;
        }

        public static RoomCard CreateAgent(CardGameComponent_Cards cards, int configId)
        {
            RoomCard card = cards.AddChild<RoomCard, int>(configId);
            card.CardType = CardType.Agent;
            card.UseCardType = UseCardType.NoTarget;
            return card;
        }
    }
}