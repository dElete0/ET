namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOf(typeof(RoomServerComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    [FriendOfAttribute(typeof(ET.Server.RoomAIComponent))]
    public class C2Room_UseCardHandler : MessageHandler<Scene, C2Room_UseCard>
    {
        protected override async ETTask Run(Scene root, C2Room_UseCard message)
        {
            await C2Room_UseCard(root.GetComponent<Room>(), message);
        }

        public static async ETTask AI2Room_UseCard(RoomAIComponent ai, C2Room_UseCard message)
        {
            await C2Room_UseCard(ai.GetParent<Room>(), message);
        } 
        private static async ETTask C2Room_UseCard(Room room, C2Room_UseCard message)
        {
            await ETTask.CompletedTask;
            CGServerUpdater updater = room.GetComponent<CGServerUpdater>();
            RoomServerComponent roomServerComponent = room.GetComponent<RoomServerComponent>();
            RoomPlayer roomPlayer = roomServerComponent.GetChild<RoomPlayer>(message.PlayerId);

            if (updater.NowPlayer != roomPlayer.Id)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.CantOperateNow });
                return;
            }

            CardGameComponent_Cards cardGameComponentCards = room.GetComponent<CardGameComponent_Cards>();
            RoomCard card = cardGameComponentCards.GetChild<RoomCard>(message.Card);
            RoomCard target = message.Target == 0 ? null : cardGameComponentCards.GetChild<RoomCard>(message.Target);
            CardGameComponent_Player playerInfo = roomPlayer.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Player enemyInfo = roomPlayer.GetEnemy().GetComponent<CardGameComponent_Player>();

            if (playerInfo.Units.Count >= CardGameComponent_Player.UnitMax) {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.CantGetMoreUnit });
                return;
            }
            if (playerInfo.Cost < card.Cost)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.NotEnoughCost });
                return;
            }

            if (card.UseCardType != UseCardType.NoTarget && target == null) {
                int playerCount = playerInfo.Units.Count;
                int enemyCount = enemyInfo.Units.Count;
                if (card.UseCardType == UseCardType.ToUnit && playerCount + enemyCount < 1) {
                    
                } else if (card.UseCardType == UseCardType.ToMyUnit && playerCount < 1) {
                    
                } else if (card.UseCardType == UseCardType.ToEnemyUnit && enemyCount < 1) {
                    
                }
                return;
            }
            RoomEventTypeComponent roomEventTypeComponent = roomPlayer.GetParent<Room>().GetComponent<RoomEventTypeComponent>();
            roomEventTypeComponent.CountClear();
            roomEventTypeComponent.Event_UseCard(new EventInfo(), roomPlayer, card, target, message.Pos);
        }
    }
}