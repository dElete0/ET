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
    }
}