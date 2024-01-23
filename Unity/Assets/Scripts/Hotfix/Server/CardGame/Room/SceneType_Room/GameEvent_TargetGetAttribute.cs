
using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_TargetGetAttribute
    {
        public static async ETTask ToDo_TargetGetAttribute(this RoomEventTypeComponent room, RoomCard actor, RoomCard target, int num)
        {
            await ETTask.CompletedTask;
            target.Attack += num;
            target.HP += num;
            target.HPMax += num;

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = target.RoomCard2UnitInfo() };
        }

        public static async ETTask ToDo_UnitsGetAttribute(this RoomEventTypeComponent room, RoomCard actor, int num, int type) {
            CardGameComponent_Cards cards = actor.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCard> units = null;
            switch (type) {
                case 1:
                    units = actor.GetOwner().GetAllMyUnits(cards);
                    break;
                case 2:
                    units = actor.GetOwner().GetEnemy().GetAllMyUnits(cards);
                    break;
                case 3:
                    units = actor.GetOwner().GetAllUnits(cards);
                    break;
            }

            foreach (var unit in units) {
                unit.Attack += num;
                unit.HP += num;
                unit.HPMax += num;
            }

            Room2C_FlashUnits message = new Room2C_FlashUnits();
            RoomMessageHelper.BroadCast(room.GetParent<Room>(), message);
            await ETTask.CompletedTask;
        }

        public static async ETTask ToDo_UnitsInGroupGetAttribute(this RoomEventTypeComponent room, RoomCard actor, int baseId, int num) {
            CardGameComponent_Cards cards = actor.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCard> roomCards = actor.GetOwner().GetAllMyCardsInGroupByBaseId(cards, baseId);
            foreach (var card in roomCards) {
                card.Attack += num;
                card.HP += num;
                card.HPMax += num;
            }
            await ETTask.CompletedTask;
        }
        
        public static async ETTask ToDo_UnitsInGroupLoseAttributeAddDamageEnemyHero(this RoomEventTypeComponent room, EventInfo eventInfo, RoomCard actor, int baseId, int num) {
            CardGameComponent_Cards cards = actor.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<RoomCard> roomCards = actor.GetOwner().GetAllMyCardsInGroupByBaseId(cards, baseId);
            int totalNum = 0;
            foreach (var card in roomCards) { 
                if (card.Attack >= num) {
                    totalNum += num;
                    card.Attack -= num;
                } else {
                    totalNum += card.Attack;
                    card.Attack = 0;
                }
                
                if (card.HP > num) {
                    totalNum += num;
                    card.HP -= num;
                } else {
                    totalNum += card.HP - 1;
                    card.HP = 1;
                }
                card.HPMax = card.HPMax > num? card.HPMax - num : 1;
            }

            await room.BroadEvent(GameEventFactory.Damage(room, actor, actor.GetOwner().GetEnemy().GetHero(), totalNum), eventInfo);
        }
    }
}