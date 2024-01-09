namespace ET.Server
{
    [MessageHandler(SceneType.RoomRoot)]
    [FriendOfAttribute(typeof(ET.Server.RoomAIComponent))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
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
            await ETTask.CompletedTask;
            RoomEventTypeComponent roomEventTypeComponent = room.GetComponent<RoomEventTypeComponent>();
            CGServerUpdater serverUpdater = room.GetComponent<CGServerUpdater>();
            RoomPlayer roomPlayer = room.GetComponent<RoomServerComponent>().GetChild<RoomPlayer>(message.PlayerId);
            

            //非当前操作者回合
            if (serverUpdater.NowPlayer != roomPlayer.Id)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
                return;
            }
            
            CardGameComponent_Cards cardGameComponentCards = room.GetComponent<CardGameComponent_Cards>();
            RoomCard card = cardGameComponentCards.GetChild<RoomCard>(message.Actor);

            if (card == null)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
                return;
            }

            RoomCard target = cardGameComponentCards.GetChild<RoomCard>(message.Target);
            RoomPlayer enemy = roomPlayer.GetEnemy();
            CardGameComponent_Player enemyInfo = enemy.GetComponent<CardGameComponent_Player>();

            //攻击次数不足
            if (card.AttackCount < 1)
            {
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
                return;
            }
            if (card.Attack < 1)
            {
                //攻击为0
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.CantOperateNow
                });
                return;
            }

            if (card.IsCallThisTurn && (
                    card.AttributePowers.Contains(Power_Type.Charge) ||
                    card.AttributePowers.Contains(Power_Type.Rush)))
            {
                //刚上场
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer,
                    new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.CantOperateNow });
                return;
            }
            if (card.IsCallThisTurn &&
                !card.AttributePowers.Contains(Power_Type.Charge) &&
                card.AttributePowers.Contains(Power_Type.Rush) &&
                (target.CardType == CardType.Agent || target.CardType == CardType.Hero))
            {
                //突袭不能攻击英雄和干员
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer,
                    new Room2C_OperateFail() { FailId = (int)Room2C_OperateFailType.CantOperateNow });
                return;
            }

            bool isEnemyHaveTaunt = false;
            foreach (var unitID in enemyInfo.Units)
            {
                if (cardGameComponentCards.GetChild<RoomCard>(unitID).AttributePowers.Contains(Power_Type.Taunt)) {
                    isEnemyHaveTaunt = true;
                    break;
                }
            }
            if (isEnemyHaveTaunt && !target.AttributePowers.Contains(Power_Type.Taunt)) {
                //必须先攻击嘲讽
                RoomMessageHelper.ServerSendMessageToClient(roomPlayer, new Room2C_OperateFail()
                {
                    FailId = (int)Room2C_OperateFailType.MustAttackTaunt
                });
                return;
            }
            roomEventTypeComponent.CountClear();
            roomEventTypeComponent.Event_AttackTo(card, target);
        }
    }
}