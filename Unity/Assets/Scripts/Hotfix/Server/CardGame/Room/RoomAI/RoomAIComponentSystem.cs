using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(RoomAIComponent))]
    [FriendOf(typeof(RoomAIComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static partial class RoomAIComponentSystem
    {
        [EntitySystem]
        private static void Awake(this RoomAIComponent self)
        {
        }

        [EntitySystem]
        private static void Update(this RoomAIComponent self)
        {
            if (self.GetParent<Room>().GetComponent<CGServerUpdater>() == null) return;
            if (self.IsToDo) return;
            if (self.GetParent<Room>().GetComponent<CGServerUpdater>().NowPlayer == self.GetParent<RoomPlayer>().Id)
            {
                self.IsToDo = true;
                self.ToDo().Coroutine();
            }
        }

        private static async ETTask ToDo(this RoomAIComponent self) {
            RoomPlayer player = self.GetParent<RoomPlayer>();
            CardGameComponent_Player cards = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards allCards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            foreach (var handCardId in cards.HandCards)
            {
                RoomCard handCard = allCards.GetChild<RoomCard>(handCardId);
                if (handCard.CardType == CardType.Unit && handCard.Cost <= cards.Cost) {
                    await C2Room_UseCardHandler.AI2Room_UseCard(self, new C2Room_UseCard() {Card = handCardId} );
                }
                break;
            }


            Log.Warning("AI自己结束自己回合");
            await C2Room_TurnOverHandler.AI2Room_TurnOver(self, new C2Room_TurnOver() { });
            self.IsToDo = false;
        }
    }
}