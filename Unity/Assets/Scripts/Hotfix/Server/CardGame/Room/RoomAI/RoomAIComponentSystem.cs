using System;
using System.Collections.Generic;
using System.Linq;

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
            CardGameComponent_Player myCards = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Player enemyCards = player.GetEnemy().GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards allCards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCard> handCards = myCards.GetHandCards(allCards);
            List<RoomCard> myUnits = myCards.GetUnits(allCards);
            List<RoomCard> enemyUnits = enemyCards.GetUnits(allCards);
            long targetId = 0;
            //有嘲讽，计算法术斩杀，否则计算所有斩杀
            //解嘲讽, 再计算斩杀
            //手牌<4且有过牌，用过牌
            //计算自己的所有对干员伤害，能解就解
            //计算自己对对单位伤害，能解就解
            //尽可能占场
            //尽可能走脸
            //多余的费打无tag的牌
            List<RoomCard> taunt = enemyUnits.GetUnitsByPower(Power_Type.Taunt);
            if (taunt.Count > 0) {
                
            } else {
                int attackHurt = myUnits.GetHurtByAttack(); 
                var handHurts = handCards.GetHurtByHandCards(myCards.Cost);
                if (attackHurt + handHurts.Item1 >= allCards.GetChild<RoomCard>(enemyCards.Hero).HP) {
                    // todo 直接斩杀
                    return;
                }
            }
            
            
            /*foreach (var handCardId in myCards.HandCards)
            {
                RoomCard handCard = allCards.GetChild<RoomCard>(handCardId);
                if (handCard.CardType == CardType.Unit && handCard.Cost <= myCards.Cost) {
                    if (handCard.UseCardType == UseCardType.ToActor) {
                        if (enemyCards.Units.Count > 0) {
                            targetId = enemyCards.Units[0];
                        } else {
                            targetId = enemyCards.Hero;
                        }
                    }
                    await C2Room_UseCardHandler.AI2Room_UseCard(self, new C2Room_UseCard() {Card = handCardId, Target = targetId} );
                }
                break;
            }*/

            Log.Warning("AI自己结束自己回合");
            await C2Room_TurnOverHandler.AI2Room_TurnOver(self, new C2Room_TurnOver() { });
            self.IsToDo = false;
        }
    }
}