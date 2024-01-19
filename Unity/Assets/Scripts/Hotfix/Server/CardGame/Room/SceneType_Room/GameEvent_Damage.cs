using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_Damage
    {
        public static async ETTask ToDo_HeroDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count)
        {
            await roomEventTypeComponent.ToDo_DamageTargets(eventInfo, card, new List<long>() {target.Id}, count);
        }

        public static async ETTask ToDo_UnitDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count)
        {
            await roomEventTypeComponent.ToDo_DamageTargets(eventInfo, card, new List<long>() {target.Id}, count);
        }

        public static async ETTask ToDo_AgentDamage(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int count)
        {
            await roomEventTypeComponent.ToDo_DamageTargets(eventInfo, card, new List<long>() {target.Id}, count);
        }

        public static async ETTask ToDo_DamageAllUnit(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomPlayer player, int num)
        {
            await ETTask.CompletedTask;
            CardGameComponent_Player myCards = player.GetComponent<CardGameComponent_Player>();
            List<long> units = myCards.GetAllUnits();

            await roomEventTypeComponent.ToDo_DamageTargets(eventInfo, actor, units, num);
        }

        public static async ETTask ToDo_Desecrate(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int num, int loopCount)
        {
            await ETTask.CompletedTask;
            Room room = roomEventTypeComponent.GetParent<Room>();
            RoomPlayer player = actor.GetOwner();
            CardGameComponent_Player myCards = player.GetComponent<CardGameComponent_Player>();
            List<long> units = myCards.GetAllUnits();

            int deadCount = eventInfo.DeadList.Count;
            await roomEventTypeComponent.ToDo_DamageTargets(eventInfo, actor, units, num);
            bool isLoop = deadCount < eventInfo.DeadList.Count;

            if (isLoop && loopCount > 1)
            {
                eventInfo.PowerStructs.Add((new Power_Struct()
                {
                    PowerType = Power_Type.Desecrate,
                    TriggerPowerType = TriggerPowerType.Release,
                    Count1 = num,
                    Count2 = loopCount - 1,
                }, null, null, player));
            }
        }

        private static async ETTask ToDo_DamageTargets(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, List<long> targetIds, int num)
        {
            await ETTask.CompletedTask;
            Room room = roomEventTypeComponent.GetParent<Room>();
            CardGameComponent_Cards cards = room.GetComponent<CardGameComponent_Cards>();

            List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
            List<int> hurts = new List<int>();

            foreach (var targetId in targetIds)
            {
                RoomCard target = cards.GetChild<RoomCard>(targetId);
                if (target.UnitType == CardUnitType.ExclusionZone) continue;
                int totalHurt = 0;
                int hpHurt = 0;
                if (target.AttributePowers.Contains(Power_Type.Bubbles)) {
                    target.AttributePowers.Remove(Power_Type.Bubbles);
                } else {
                    if (target.CardType == CardType.Hero)
                    {
                        if (target.Armor >= num) {
                            target.Armor -= num;
                            hpHurt = 0;
                            totalHurt = num;
                        } else if (target.Armor > 0) {
                            hpHurt = num - target.Armor;
                            totalHurt = num;
                            target.Armor = 0;
                        } else {
                            totalHurt = num;
                            hpHurt = num;
                        }
                    } else {
                        totalHurt = num;
                        hpHurt = num;
                    }
                }

                if (totalHurt > 0) {
                    target.HP -= hpHurt;
                    cardInfos.Add(target.RoomCard2UnitInfo());
                    hurts.Add(totalHurt);
                    if (target.HP < 1 && (target.CardType == CardType.Unit || target.CardType == CardType.Agent)) {
                        //Log.Warning($"添加死亡单位:{target.Id}");
                        eventInfo.DeadList.Add(target);
                    }
                }
            }

            if (hurts.Count > 0) {
                // Todo 客户端执行受伤动作
                Room2C_CardsGetDamage cardGetDamage = new() { Card = cardInfos, hurt = hurts };
                RoomMessageHelper.BroadCast(room, cardGetDamage);
            }
        }
    }
}