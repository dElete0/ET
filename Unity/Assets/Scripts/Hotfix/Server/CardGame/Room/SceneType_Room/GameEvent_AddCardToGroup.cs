using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_AddCardToGroup
    {
        public static async ETTask ToDo_AddCardToGroup(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int baseId, int num, int att, bool isShow)
        {
            RoomPlayer player = actor.GetOwner();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards cards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();

            List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
            for (int i = 0; i < num; i++) {
                if (playerInfo.Groups.Count >= CardGameMsg.GroupMax) {
                    break;
                }
                RoomCard card = RoomCardFactory.CreateGroupCard(cards, baseId, player.Id);
                if (att > 0) {
                    card.Attack = att;
                    card.AttackD = att;
                    card.HP = att;
                    card.HPD = att;
                    card.HPMax = att;
                }
                cardInfos.Add(card.RoomCard2UnitInfo());
                playerInfo.Groups.Add(card.Id);
            }

            if (cardInfos.Count > 0) {
                if (isShow) {
                    Room2C_AddCardsToGroupShow myMessage = new Room2C_AddCardsToGroupShow() { Cards = cardInfos, IsMy = true };
                    Room2C_AddCardsToGroupShow enemyMessage = new Room2C_AddCardsToGroupShow() { Cards = cardInfos, IsMy = false };
                    RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
                    RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
                } else {
                    Room2C_AddCardsToGroupHide myMessage = new Room2C_AddCardsToGroupHide() { Num = cardInfos.Count, IsMy = true };
                    Room2C_AddCardsToGroupHide enemyMessage = new Room2C_AddCardsToGroupHide() { Num = cardInfos.Count, IsMy = false };
                    RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
                    RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
                }
            }
        }
    }
}