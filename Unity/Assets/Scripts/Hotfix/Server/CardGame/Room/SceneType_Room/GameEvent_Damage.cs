using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Damage
    {
        public static void ToDo_HeroDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count)
        {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
            
            
            if (target.HP < 1) {
                eventInfo.DeadList.Add(target);
            }
        }

        public static void ToDo_UnitDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
            
            if (target.HP < 1) {
                Log.Warning($"添加死亡单位:{target.Id}");
                eventInfo.DeadList.Add(target);
            }
        }

        public static void ToDo_AgentDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count) {
            target.HP -= count;
            
            // Todo 客户端执行受伤动作
            Room room = roomEventTypeComponent.GetParent<Room>();
            Room2C_CardGetDamage cardGetDamage = new() { Card = target.RoomCard2UnitInfo(), hurt = count};
            RoomMessageHelper.BroadCast(room, cardGetDamage);

            if (target.HP < 1) {
                eventInfo.DeadList.Add(target);
            }
        }

        public static void ToDo_DamageAllUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomPlayer player, int count) {
            Room room = roomEventTypeComponent.GetParent<Room>();
            CardGameComponent_Player myCards = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Cards cards = room.GetComponent<CardGameComponent_Cards>();
            List<long> units = myCards.GetAllUnits();
            List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
            List<int> hurts = new List<int>();
            foreach (var unitId in units) {
                RoomCard unit = cards.GetChild<RoomCard>(unitId);
                unit.HP -= count;
                
                cardInfos.Add(unit.RoomCard2UnitInfo());
                hurts.Add(count);
                
                if (unit.HP < 1) {
                    eventInfo.DeadList.Add(unit);
                }
            }
            
            // Todo 客户端执行受伤动作
            Room2C_CardsGetDamage cardGetDamage = new() { Card = cardInfos, hurt = hurts};
            RoomMessageHelper.BroadCast(room, cardGetDamage);
        }
    }
}