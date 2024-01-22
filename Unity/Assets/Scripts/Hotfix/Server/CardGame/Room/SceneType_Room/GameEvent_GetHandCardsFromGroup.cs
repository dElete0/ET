using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_GetHandCardsFromGroup
    {
        public static async ETTask ToDo_GetHandCardsFromGroup(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, int count) {
            await ETTask.CompletedTask;
            Room room = roomEventTypeComponent.GetParent<Room>();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();

            for (int i = 0; i < count; i++)
            {
                long card = playerInfo.Groups[0];
                RoomCard roomCard = null;
                if (playerInfo.Groups.Count > 0) {
                    roomCard = room.GetComponent<CardGameComponent_Cards>().GetChild<RoomCard>(card);

                    if (roomCard == null)
                    {
                        Log.Error("没得这张卡");
                    }
                    
                    if (playerInfo.HandCards.Count >= playerInfo.HandCardsCountMax) {
                        // todo 爆牌
                    } else {
                        // Client
                        // 通知客户端展示抽到的牌
                        RoomCardInfo myCard = roomCard.RoomCard2MyHandCardInfo();
                        RoomCardInfo enemyCard = roomCard.RoomCard2EnemyHandCardInfo();
                        Room2C_EnemyGetHandCardFromGroup room2C_EnemyGetHandCardsFromGroup = new() { CardInfo = enemyCard };
                        RoomMessageHelper.BroadCastWithOutPlayer(player, room2C_EnemyGetHandCardsFromGroup);
                        Room2C_GetHandCardFromGroup room2C_GetHandCardFromGroup = new() { CardInfo = myCard };
                        RoomMessageHelper.ServerSendMessageToClient(player, room2C_GetHandCardFromGroup);
                        
                        // Log.Debug("展示抽卡:" + i + "/" + count);
                        await roomEventTypeComponent.BroadEvent(GameEventFactory.GetHandCardFromShowCard(roomEventTypeComponent, player, card), eventInfo);
                        await roomEventTypeComponent.BroadEvent(GameEventFactory.RemoveCardFromGroup(roomEventTypeComponent, player, card), eventInfo);

                        if (roomCard.AttributePowers.ContainsKey(Power_Type.UseCardWhenGetThisCardFromGroup)) {
                            await roomEventTypeComponent.BroadEvent(GameEventFactory.PowerToUseCard(roomEventTypeComponent, roomCard, roomCard), eventInfo);
                        }
                    }
                } else {
                    // todo 抽疲劳
                }
            }
        }
    }
}