namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOf(typeof(RoomServerComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public class C2Room_UseCardHandler : MessageHandler<Scene, C2Room_UseCard>
    {
        protected override async ETTask Run(Scene root, C2Room_UseCard message)
        {
            await ETTask.CompletedTask;
            Room room = root.GetComponent<Room>();
            CGServerUpdater updater = room.GetComponent<CGServerUpdater>();
            RoomServerComponent roomServerComponent = room.GetComponent<RoomServerComponent>();
            RoomPlayer roomPlayer = roomServerComponent.GetChild<RoomPlayer>(message.PlayerId);

            if (updater.NowPlayer != roomPlayer.Id) {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.CantOperateNow });
                return;
            }

            CardGameComponent_Cards cardGameComponentCards = room.GetComponent<CardGameComponent_Cards>();
            RoomCard card = cardGameComponentCards.GetChild<RoomCard>(message.Card);
            RoomCard target = message.Target == 0 ? null : cardGameComponentCards.GetChild<RoomCard>(message.Target);
            CardGameComponent_Player playerInfo = roomPlayer.GetComponent<CardGameComponent_Player>();

            if (playerInfo.Cost < card.Cost)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.NotEnoughCost });
                return;
            }
            RoomEventTypeComponent roomEventTypeComponent = roomPlayer.GetParent<Room>().GetComponent<RoomEventTypeComponent>();
            roomEventTypeComponent.CountClear();
            roomEventTypeComponent.Event_UseCard(roomPlayer, card, target, 0);
        }
    }
}