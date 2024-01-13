using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static class GameEvent_CallUnit
    {
        public static void ToDo_UnitInFight(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card)
        {
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            card.IsCallThisTurn = true;
            //回合开始时，攻击计数清空
            card.GetComponent<CardEventTypeComponent>().UnitGameEventTypes.Add(TriggerEventFactory.TurnStart(), new GameEvent(GameEventType.TurnStart)
            {
                ToDo = (info) => {
                    card.AttackCount = card.AttackCountMax;
                    card.IsCallThisTurn = false;
                }
            });

            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.UnitBeCalled(roomEventTypeComponent, card, player), eventInfo);
        }

        public static void ToDo_UnitStand(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, RoomCard card, int pos) {
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            playerCards.Units.Insert(pos, card.Id);
            
            // Log.Warning($"单位站场:{card.Id}");
            // Log.Warning($"向客户端发送创建消息:{playerCards.Units.Count}");
            List<long> order = new List<long>(playerCards.Units);
            Room2C_CallUnit room2CCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = order };
            RoomMessageHelper.ServerSendMessageToClient(player, room2CCallUnit);

            Room2C_EnemyCallUnit room2CEnemyCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = order };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2CEnemyCallUnit);
        }
    }
}
