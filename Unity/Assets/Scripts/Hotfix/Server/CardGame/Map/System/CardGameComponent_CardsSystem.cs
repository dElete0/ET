using System.Collections.Generic;

namespace ET.Server {
    [EntitySystemOf(typeof(CardGameComponent_Cards))]
    [FriendOfAttribute(typeof(ET.CardGameComponent_Cards))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static partial class CardGameComponent_CardsSystem
    {
        [EntitySystem]
        private static void Awake(this ET.CardGameComponent_Cards self)
        {
        }

        public static void RemoveCard(this CardGameComponent_Cards self, RoomCard card)
        {
            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            foreach (RoomPlayer player in roomServerComponent.Children.Values)
            {
                CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
                if (playerCards.HandCards.Contains(card.Id)) 
                    playerCards.HandCards.Remove(card.Id);
                if (playerCards.Units.Contains(card.Id))
                    playerCards.Units.Remove(card.Id);
                if (playerCards.Groups.Contains(card.Id))
                    playerCards.Groups.Remove(card.Id);
                self.RemoveChild(card.Id);
            }
        }

        public static bool IsHandCards(this CardGameComponent_Cards self, RoomCard card) {
            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            foreach (RoomPlayer player in roomServerComponent.Children.Values)
            {
                CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
                if (playerCards.HandCards.Contains(card.Id))
                    return true;
            }
            return false;
        }

        public static bool IsUnit(this CardGameComponent_Cards self, RoomCard card) {
            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            foreach (RoomPlayer player in roomServerComponent.Children.Values)
            {
                CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
                if (playerCards.Units.Contains(card.Id))
                    return true;
            }
            return false;
        }
        
        public static bool IsGroup(this CardGameComponent_Cards self, RoomCard card) {
            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            foreach (RoomPlayer player in roomServerComponent.Children.Values)
            {
                CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
                if (playerCards.Groups.Contains(card.Id))
                    return true;
            }
            return false;
        }
    }
}