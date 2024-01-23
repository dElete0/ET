using System.Collections.Generic;
using System.Linq;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static class GameEvent_CallUnit
    {
        public static async ETTask ToDo_UnitInFight(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card)
        {
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            card.IsCallThisTurn = true;

            //告知监听器，有单位被召唤了
            await roomEventTypeComponent.BroadEvent(GameEventFactory.CallUnitOver(roomEventTypeComponent, card, player), eventInfo);
        }

        public static async ETTask ToDo_UnitStand(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, RoomCard card, int pos) {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            playerCards.Units.Insert(pos, card.Id);
            
            Log.Warning($"单位站场:{card.Id}");
            // Log.Warning($"向客户端发送创建消息:{playerCards.Units.Count}");
            List<long> order = new List<long>(playerCards.Units);
            Room2C_CallUnit room2CCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = order };
            RoomMessageHelper.ServerSendMessageToClient(player, room2CCallUnit);

            Room2C_EnemyCallUnit room2CEnemyCallUnit = new() { Card = card.RoomCard2UnitInfo(), UnitOrder = order };
            RoomMessageHelper.BroadCastWithOutPlayer(player, room2CEnemyCallUnit);
        }

        public static async ETTask ToDo_CallTargetUnitForAllByBaseId(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int baseId, int num, int att) {
            await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnitByBaseId(roomEventTypeComponent, actor.GetOwner(), actor, baseId, num), eventInfo);
            await roomEventTypeComponent.BroadEvent(GameEventFactory.CallTargetUnitByBaseId(roomEventTypeComponent, actor.GetOwner().GetEnemy(), actor, baseId, num), eventInfo);
        }

        public static async ETTask ToDo_CalltargetUnitByBaseId(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard actor, int baseId, int num, CallType type) {
            await ETTask.CompletedTask;
            CardGameComponent_Player playerCards = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards cards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCardInfo> unitInfos = new List<RoomCardInfo>();
            List<RoomCard> units = new List<RoomCard>();
            //执行站场逻辑
            if (actor is { CardType: CardType.Unit }) {
                //触发召唤的卡牌的位置
                int j = 0;
                foreach (var unit in playerCards.Units) {
                    if (unit == actor.Id) {
                        break;
                    }
                    j++;
                }
                //第一个创建在右边
                bool isleft = false;
                for (int i = 0; i < num; i++) {
                    if (playerCards.Units.Count >= CardGameMsg.UnitMax) {
                        break;
                    }
                    RoomCard card = RoomCardFactory.CreateGroupCard(cards, baseId, player.Id);
                    card.SetRoomCardByCallType(type, playerCards);
                    unitInfos.Add(card.RoomCard2UnitInfo());
                    units.Add(card);
                    await roomEventTypeComponent.ToDo_CallTargetUnit(eventInfo, player, card, (j + (isleft? 1: 0)));
                    isleft = !isleft;
                    if (!isleft) {
                        j++;
                    }
                }
            } else {
                for (int i = 0; i < num; i++) {
                    if (playerCards.Units.Count >= CardGameMsg.UnitMax) {
                        break;
                    }
                    RoomCard card = RoomCardFactory.CreateGroupCard(cards, baseId, player.Id);
                    card.SetRoomCardByCallType(type, playerCards);
                    unitInfos.Add(card.RoomCard2UnitInfo());
                    units.Add(card);
                    await roomEventTypeComponent.ToDo_CallTargetUnit(eventInfo, player, card, playerCards.Units.Count);
                }
            }
        }

        public static async ETTask ToDo_SendUnitToEnemy(this RoomEventTypeComponent room, EventInfo info, RoomCard actor, RoomCard target) {
            info.RemoveList.Add((target, RemoveType.SendToEnemy));
            info.PowerStructs.Add((new Power_Struct() {
                PowerType = Power_Type.CallTargetUnit,
                Count1 = actor.GetOwner().GetEnemy().GetComponent<CardGameComponent_Player>().Units.Count,
            }, actor, target, null, actor.GetOwner().GetEnemy()));
            await ETTask.CompletedTask;
        }
        
        public static async ETTask ToDo_CallTargetUnit(this RoomEventTypeComponent room, EventInfo info, RoomPlayer player, RoomCard card, int pos) {
            await room.ToDo_UnitStand(player, card, pos);
            if (card.GetAura().Count > 0) {
                await room.BroadEvent(GameEventFactory.AuraEffect(room, card, player), info);
            }
            await room.ToDo_UnitInFight(info, player, card);
        }

        private static void SetRoomCardByCallType(this RoomCard card, CallType type, CardGameComponent_Player playerCards) {
            if (type == CallType.RedDragon) {
                card.AttackD += playerCards.RedGragonNum;
                card.Attack += playerCards.RedGragonNum;
                card.HPD += playerCards.RedGragonNum;
                card.HP += playerCards.RedGragonNum;
                card.HPMax += playerCards.RedGragonNum;
                playerCards.RedGragonNum++;
            }
        }
    }
}
