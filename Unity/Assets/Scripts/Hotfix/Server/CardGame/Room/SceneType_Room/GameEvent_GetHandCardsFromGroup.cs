using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_GetHandCardsFromGroup
    {
        public static void ToDo_GetHandCardsFromGroup(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, int count)
        {
            Room room = roomEventTypeComponent.GetParent<Room>();
            CardGameComponent_Player cards = player.GetComponent<CardGameComponent_Player>();
            List<long> group = cards.Groups;
            List<long> handCards = cards.HandCards;

            for (int i = 0; i < count; i++)
            {
                long card = group[0];
                RoomCard roomCard = null;
                if (group.Count > 0)
                {
                    roomCard = room.GetComponent<CardGameComponent_Cards>().GetChild<RoomCard>(card);
                }
                else
                {
                    // todo 抽疲劳
                }

                if (roomCard == null)
                {
                    Log.Error("没得这张卡");
                }

                Log.Debug("展示抽卡:" + i + "/" + count);
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCard(roomEventTypeComponent, player, card));
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.RemoveCardFromGroup(roomEventTypeComponent, player, card));

                RoomCardInfo myCard = roomCard.RoomCard2MyHandCardInfo();

                RoomCardInfo enemyCard = roomCard.RoomCard2EnemyHandCardInfo();
                
                // Client
                // 通知客户端展示抽到的牌
                Room2C_EnemyGetHandCardFromGroup room2C_EnemyGetHandCardsFromGroup = new() { CardInfo = enemyCard };
                RoomMessageHelper.BroadCastWithOutPlayer(player, room2C_EnemyGetHandCardsFromGroup);

                Room2C_GetHandCardFromGroup room2C_GetHandCardFromGroup = new() { CardInfo = myCard };
                RoomMessageHelper.ServerSendMessageToClient(player, room2C_GetHandCardFromGroup);
            }
        }
    }
}