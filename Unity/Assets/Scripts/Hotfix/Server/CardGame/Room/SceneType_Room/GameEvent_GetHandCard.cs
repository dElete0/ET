using System;
using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static class GameEvent_GetHandCard
    {
        public static async ETTask ToDo_GetHandCardFromShowCard(this RoomEventTypeComponent room, RoomPlayer player, long card)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            // Log.Debug("展示的卡加入手牌");
            playerInfo.HandCards.Add(card);

            RoomCard roomCard = room.GetParent<Room>().GetComponent<CardGameComponent_Cards>().GetChild<RoomCard>(card);

            // Client
            // 通知客户端将抽到的展示牌置入手牌
            Room2C_EnemyGetHandCardFromShowCard room2C_EnemyGetHandCardFromShowCard = new() { CardId = card };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2C_EnemyGetHandCardFromShowCard);

            Room2C_GetHandCardFromShowCard room2C_GetHandCardFromShowCard = new() { CardId = card };
            RoomMessageHelper.ServerSendMessageToClient(player, room2C_GetHandCardFromShowCard);
        }

        public static async ETTask ToDo_GetHandCards(this RoomEventTypeComponent room, RoomPlayer player, List<RoomCard> cards)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            // Log.Debug("展示的卡加入手牌");
            foreach (var card in cards)
            {
                playerInfo.HandCards.Add(card.Id);
            }

            // Client
            // 通知客户端将抽到的展示牌置入手牌
            Room2C_GetHandCards myMessage = new() { Cards = cards.GetRoomCardInfos(1) };
            RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
            Room2C_EnemyGetHandCards enemyMessage = new() { Cards = cards.GetRoomCardInfos(2) };
            RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
        }
        
        public static async ETTask ToDo_GetHandCardsByBaseIds(this RoomEventTypeComponent room, RoomPlayer player, int baseId, int num, int att)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards cards = room.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCard> roomCards = new List<RoomCard>();
            for (int i = 0; i < num; i++) {
                RoomCard card = cards.AddChild<RoomCard, int, long>(baseId, player.Id);
                roomCards.Add(card);
                playerInfo.HandCards.Add(card.Id);
            }

            // Client
            // 通知客户端将抽到的展示牌置入手牌
            Room2C_GetHandCards myMessage = new() { Cards = roomCards.GetRoomCardInfos(1) };
            RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
            Room2C_EnemyGetHandCards enemyMessage = new() { Cards = roomCards.GetRoomCardInfos(2) };
            RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
        }

        public static async ETTask ToDo_FindAndCloneCard(this RoomEventTypeComponent room, EventInfo eventInfo, RoomCard actor, int num, int type)
        {
            RoomPlayer player = actor.GetOwner();
            CardGameComponent_Cards cards = room.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<long> findList = null;
            switch (type)
            {
                case 1:
                    findList = new List<long>(player.GetComponent<CardGameComponent_Player>().Groups);
                    break;
            }

            if (findList == null || findList.Count < 1)
            {
                return;
            }

            findList.RandomList();
            List<long> threeCardIds = findList.Count < 4 ? findList : new List<long>() { findList[0], findList[1], findList[2] };
            List<RoomCardInfo> threeCards = new List<RoomCardInfo>();
            foreach (var cardId in threeCardIds)
            {
                threeCards.Add(cards.GetChild<RoomCard>(cardId).RoomCard2MyHandCardInfo());
            }
            Room2C_FindCardsToShow message = new Room2C_FindCardsToShow() { Cards = threeCards };
            RoomMessageHelper.ServerSendMessageToClient(player, message);

            WaitType.Wait_C2Room_Select response = await room.GetParent<Room>().GetComponent<ObjectWait>().Wait<WaitType.Wait_C2Room_Select>();
            RoomCard baseCard = cards.GetChild<RoomCard>(response.Message.CardId);
            RoomCard card = baseCard.CloneTargetCard(cards, baseCard.PlayerId);
            await room.BroadEvent(GameEventFactory.GetHandCards(room, player, card, null ), eventInfo);
            Log.Warning(num);
            if (num > 1)
            {
                eventInfo.PowerStructs.Add((new Power_Struct()
                {
                    PowerType = Power_Type.FindAndCloneCard,
                    TriggerPowerType = TriggerPowerType.Release,
                    Count1 = num - 1,
                    Count2 = type,
                }, actor, null, null, player));
            }
        }

        public static async ETTask ToDo_TargetBackToHandCards(this RoomEventTypeComponent room, EventInfo eventInfo, RoomCard actor, RoomCard target)
        {
            eventInfo.RemoveList.Add((target, RemoveType.BackToHandCards));
            eventInfo.PowerStructs.Add((new Power_Struct()
            {
                PowerType = Power_Type.GetHandCardsByBaseIds,
                TriggerPowerType = TriggerPowerType.Release,
                Count1 = target.ConfigId,
                Count2 = 1,
            }, actor, target, null, target.GetOwner()));
            await ETTask.CompletedTask;
        }

        public static async ETTask ToDo_TargetBackToGroup(this RoomEventTypeComponent room, EventInfo eventInfo, RoomCard actor, RoomCard target) {
            eventInfo.RemoveList.Add((target, RemoveType.TargetBackToGroup));
            eventInfo.PowerStructs.Add((new Power_Struct()
            {
                PowerType = Power_Type.AddCardToGroupByBaseIdShow,
                TriggerPowerType = TriggerPowerType.Release,
                Count1 = target.ConfigId,
                Count2 = 1,
            }, actor, null, null, target.GetOwner()));
            await ETTask.CompletedTask;
        }

        public static List<RoomCardInfo> GetRoomCardInfos(this List<RoomCard> cards, int type)
        {
            List<RoomCardInfo> roomCardInfos = new List<RoomCardInfo>();
            foreach (var card in cards)
            {
                switch (type)
                {
                    case 1:
                        roomCardInfos.Add(card.RoomCard2MyHandCardInfo());
                        break;
                    case 2:
                        roomCardInfos.Add(card.RoomCard2EnemyHandCardInfo());
                        break;
                }
            }
            return roomCardInfos;
        }

        public static RoomCard CloneTargetCard(this RoomCard baseCard, CardGameComponent_Cards cards, long playerId)
        {
            RoomCard card = cards.AddChild<RoomCard, int, long>(baseCard.ConfigId, playerId);
            card.Attack = baseCard.Attack;
            card.HP = baseCard.HP;
            card.HPMax = baseCard.HPMax;
            card.Red = baseCard.Red;
            card.Green = baseCard.Green;
            card.Grey = baseCard.Grey;
            card.Blue = baseCard.Blue;
            card.Black = baseCard.Black;
            card.White = baseCard.White;
            card.Cost = baseCard.Cost;
            card.AttributePowers = new Dictionary<Power_Type, int>(baseCard.AttributePowers);
            card.OtherPowers = new List<Power_Struct>(baseCard.OtherPowers);
            card.GetComponent<CardEventTypeComponent>().AllGameEventTypes = new Dictionary<TriggerEvent, GameEvent>(baseCard.GetComponent<CardEventTypeComponent>().AllGameEventTypes);
            return card;
        }

        public static void RandomList<T>(this List<T> list)
        {
            Random random = new Random();
            for (int i = list.Count - 1; i > 0; --i)
            {
                int j = random.Next(i + 1);

                // 交换当前元素与随机选取的元素位置上的值
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}