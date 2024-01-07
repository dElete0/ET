namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_GetBaseColor
    {
        public static void ToDo_GetBaseColor(this RoomEventTypeComponent room, RoomPlayer player) {
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            CardColor color = playerInfo.GetColorByNum(playerInfo.NowColor);
            int num = 0;
            switch (color) {
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
            if (playerInfo.NowColor > 5) {
                playerInfo.NowColor = 0;
            }
            
            Room2C_GetColor MyColor = new Room2C_GetColor() { Color = (int)color, Num = num, IsMy = true };
            RoomMessageHelper.ServerSendMessageToClient(player, MyColor);
            Room2C_GetColor EnemyColor = new Room2C_GetColor() { Color = (int)color, Num = num, IsMy = false };
            RoomMessageHelper.BroadCastWithOutPlayer(player, EnemyColor);
        }
    }
}