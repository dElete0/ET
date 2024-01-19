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
            CardColor color = playerInfo.GetColorByNum(playerInfo.NowColor);
            int num = 0;
            switch (color)
            {
                case CardColor.Red:
                    playerInfo.Red++;
                    if (playerInfo.Red > 12)
                        playerInfo.Red = 12;
                    num = playerInfo.Red;
                    break;
                case CardColor.Blue:
                    playerInfo.Blue++;
                    if (playerInfo.Blue > 12)
                        playerInfo.Blue = 12;
                    num = playerInfo.Blue;
                    break;
                case CardColor.Green:
                    playerInfo.Green++;
                    if (playerInfo.Green > 12)
                        playerInfo.Green = 12;
                    num = playerInfo.Green;
                    break;
                case CardColor.White:
                    playerInfo.White++;
                    if (playerInfo.White > 12)
                        playerInfo.White = 12;
                    num = playerInfo.White;
                    break;
                case CardColor.Black:
                    playerInfo.Black++;
                    if (playerInfo.Black > 12)
                        playerInfo.Black = 12;
                    num = playerInfo.Black;
                    break;
                case CardColor.Grey:
                    playerInfo.Grey++;
                    if (playerInfo.Grey > 12)
                        playerInfo.Grey = 12;
                    num = playerInfo.Grey;
                    break;
            }
            playerInfo.NowColor++;
            if (playerInfo.NowColor > 5)
            {
                playerInfo.NowColor = 0;
            }

            Room2C_GetColor MyColor = new Room2C_GetColor() { Color = (int)color, Num = num, IsMy = true };
            RoomMessageHelper.ServerSendMessageToClient(player, MyColor);
            Room2C_GetColor EnemyColor = new Room2C_GetColor() { Color = (int)color, Num = num, IsMy = false };
            RoomMessageHelper.BroadCastWithOutPlayer(player, EnemyColor);
        }

        public static async ETTask ToDo_GetColor(this RoomEventTypeComponent room, RoomCard card, int count1, int count2) {
            RoomPlayer player = card.GetOwner();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            int num = 0;
            switch ((CardColor)count1) {
                case CardColor.Black:
                    playerInfo.Black++;
                    if (playerInfo.Black > 12)
                        playerInfo.Black = 12;
                    num = playerInfo.Black;
                    break;
                case CardColor.White:
                    playerInfo.White++;
                    if (playerInfo.White > 12)
                        playerInfo.White = 12;
                    num = playerInfo.White;
                    break;
                case CardColor.Red:
                    playerInfo.Red++;
                    if (playerInfo.Red > 12)
                        playerInfo.Red = 12;
                    num = playerInfo.Red;
                    break;
                case CardColor.Blue:
                    playerInfo.Blue++;
                    if (playerInfo.Blue > 12)
                        playerInfo.Blue = 12;
                    num = playerInfo.Blue;
                    break;
                case CardColor.Green:
                    playerInfo.Green++;
                    if (playerInfo.Green > 12)
                        playerInfo.Green = 12;
                    num = playerInfo.Green;
                    break;
                case CardColor.Grey:
                    playerInfo.Grey++;
                    if (playerInfo.Grey > 12)
                        playerInfo.Grey = 12;
                    num = playerInfo.Grey;
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