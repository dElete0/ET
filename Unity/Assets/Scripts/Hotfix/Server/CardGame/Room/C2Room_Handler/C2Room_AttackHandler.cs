namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOfAttribute(typeof(ET.Server.RoomAIComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public class C2Room_AttackHandler : MessageHandler<Scene, C2Room_Attack>
    {
        protected override async ETTask Run(Scene root, C2Room_Attack message)
        {
            await C2Room_Attack(root.GetComponent<Room>(), message);
        }

        public static async ETTask AI2Room_Attack(RoomAIComponent ai, C2Room_Attack message)
        {
            await C2Room_Attack(ai.GetParent<Room>(), message);
            ai.IsToDo = false;
        }

        public static async ETTask C2Room_Attack(Room room, C2Room_Attack message)
        {
            RoomEventTypeComponent roomEventTypeComponent = room.GetComponent<RoomEventTypeComponent>();
            CGServerUpdater serverUpdater = room.GetComponent<CGServerUpdater>();
            RoomPlayer roomPlayer = room.GetComponent<RoomServerComponent>().GetChild<RoomPlayer>(message.PlayerId);
            CardGameComponent_Cards cardGameComponentCards = room.GetComponent<CardGameComponent_Cards>();
            RoomCard card = cardGameComponentCards.GetChild<RoomCard>(message.Actor);
            if (card == null) {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
            } else if (card.AttackCount < 1)
            {
                //攻击次数不足
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
            }
            else if (serverUpdater.NowPlayer != roomPlayer.Id)
            {
                //非当前操作者回合
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
            }
            else
            {
                RoomCard target = cardGameComponentCards.GetChild<RoomCard>(message.Target);
                roomEventTypeComponent.CountClear();
                roomEventTypeComponent.Event_AttackTo(card, target);
            }
            await ETTask.CompletedTask;
        }
    }
}