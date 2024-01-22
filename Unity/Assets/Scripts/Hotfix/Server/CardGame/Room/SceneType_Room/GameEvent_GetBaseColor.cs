using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_GetBaseColor
    {
        public static async ETTask ToDo_GetBaseColor(this RoomEventTypeComponent room, RoomPlayer player)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            (CardColor, RoomCard) colorCard = playerInfo.GetColorByNum(playerInfo.NowColor);
            await room.ToDo_GetColor(colorCard.Item2, (int)colorCard.Item1, 1);
            playerInfo.NowColor++;
            if (playerInfo.NowColor > 5)
            {
                playerInfo.NowColor = 0;
            }
        }

        public static async ETTask ToDo_GetColor(this RoomEventTypeComponent room, RoomCard card, int count1, int count2) {
            RoomPlayer player = card.GetOwner();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            int num = 0;
            switch ((CardColor)count1) {
                case CardColor.Black:
                    playerInfo.Black += count2;
                    if (playerInfo.Black > 12)
                        playerInfo.Black = 12;
                    num = playerInfo.Black;
                    break;
                case CardColor.White:
                    playerInfo.White += count2;
                    if (playerInfo.White > 12)
                        playerInfo.White = 12;
                    num = playerInfo.White;
                    break;
                case CardColor.Red:
                    playerInfo.Red += count2;
                    if (playerInfo.Red > 12)
                        playerInfo.Red = 12;
                    num = playerInfo.Red;
                    break;
                case CardColor.Blue:
                    playerInfo.Blue += count2;
                    if (playerInfo.Blue > 12)
                        playerInfo.Blue = 12;
                    num = playerInfo.Blue;
                    break;
                case CardColor.Green:
                    playerInfo.Green += count2;
                    if (playerInfo.Green > 12)
                        playerInfo.Green = 12;
                    num = playerInfo.Green;
                    break;
                case CardColor.Grey:
                    playerInfo.Grey += count2;
                    if (playerInfo.Grey > 12)
                        playerInfo.Grey = 12;
                    num = playerInfo.Grey;
                    break;
                case CardColor.All:
                    playerInfo.Red += count2;
                    playerInfo.Grey += count2;
                    playerInfo.Black += count2;
                    playerInfo.Green += count2;
                    playerInfo.Blue += count2;
                    playerInfo.White += count2;
                    if (playerInfo.Red > 12)
                        playerInfo.Red = 12;
                    if (playerInfo.Grey > 12)
                        playerInfo.Grey = 12;
                    if (playerInfo.Black > 12)
                        playerInfo.Black = 12;
                    if (playerInfo.Green > 12)
                        playerInfo.Green = 12;
                    if (playerInfo.Blue > 12)
                        playerInfo.Blue = 12;
                    if (playerInfo.White > 12)
                        playerInfo.White = 12;
                    num = count2;
                    break;
            }
            
            Room2C_GetColor MyColor = new Room2C_GetColor() { Color = (int)count1, Num = num, IsMy = true };
            RoomMessageHelper.ServerSendMessageToClient(player, MyColor);
            Room2C_GetColor EnemyColor = new Room2C_GetColor() { Color = (int)count1, Num = num, IsMy = false };
            RoomMessageHelper.BroadCastWithOutPlayer(player, EnemyColor);
        }

        public static async ETTask ToDo_AllAttackCountClear(this CardGameComponent_Cards cardGameComponent, CardGameComponent_Player cards) {
            await ETTask.CompletedTask;
            List<long> actors = new List<long>(cards.Units);
            actors.Add(cards.Hero);
            if (cards.Agent1 != 0) actors.Add(cards.Agent1);
            if (cards.Agent2 != 0) actors.Add(cards.Agent2);
            foreach (var actor in actors)
            {
                RoomCard card = cardGameComponent.GetChild<RoomCard>(actor);
                if (card == null) Log.Error($"未获取到角色:{actor}");
                card.AttackCount = card.AttackCountMax;
                card.IsCallThisTurn = false;
            }
        }

        public static async ETTask ToDo_AttackCountClear(this RoomCard card) {
            await ETTask.CompletedTask;
            card.AttackCount = card.AttackCountMax;
            card.IsCallThisTurn = false;
        }
    }
}