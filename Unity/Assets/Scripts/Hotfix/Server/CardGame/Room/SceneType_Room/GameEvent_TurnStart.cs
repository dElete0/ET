namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    public static class GameEvent_TurnStart
    {
        public static async ETTask ToDo_TurnStart(this RoomEventTypeComponent room, RoomPlayer player)
        {
            await ETTask.CompletedTask;
            room.CountClear();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            room.GetParent<Room>().GetComponent<CGServerUpdater>().NowPlayer = player.Id;
            Log.Warning($"当前是{player.Id.ToString()}的回合");

            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_TurnStart()
            {
                IsThisClient = true,
                Cost = playerInfo.Cost,
                CostD = playerInfo.CostTotal,
                Red = playerInfo.Red,
                Blue = playerInfo.Blue,
                White = playerInfo.White,
                Grey = playerInfo.Grey,
                Green = playerInfo.Green,
                Black = playerInfo.Black,
            });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_TurnStart()
            {
                IsThisClient = false,
                Cost = playerInfo.Cost,
                CostD = playerInfo.CostTotal,
                Red = playerInfo.Red,
                Blue = playerInfo.Blue,
                White = playerInfo.White,
                Grey = playerInfo.Grey,
                Green = playerInfo.Green,
                Black = playerInfo.Black,
            });
        }
    }
}