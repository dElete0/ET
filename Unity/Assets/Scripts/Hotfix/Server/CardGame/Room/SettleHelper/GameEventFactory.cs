using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEventFactory
    {
        public static GameEvent GetHandCardsFromGroup(RoomEventTypeComponent room, RoomPlayer player, int count)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCardFromGroup);
            gameEvent.ToDo = (info) =>
            {
                room.ToDo_GetHandCardsFromGroup(info, player, count);
            };
            return gameEvent;
        }

        public static GameEvent GetCostTotal(RoomEventTypeComponent room, RoomPlayer player, int count) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCostTotal);
            gameEvent.ToDo = (info) => {
                room.ToDo_GetCostTotal(player, count);
            };
            return gameEvent;
        }

        public static GameEvent ResetCost(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.ResetCost);
            gameEvent.ToDo = (info) => {
                room.ToDo_ResetCost(player);
            };
            return gameEvent;
        }

        public static GameEvent GetBaseColor(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetBaseColor);
            gameEvent.ToDo = (info) => {
                room.ToDo_GetBaseColor(player);
            };
            return gameEvent;
        }

        public static GameEvent GetHandCard(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = (info) =>
            {
                room.ToDo_GetHandCard(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent RemoveCardFromGroup(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveCardFromGroup);
            gameEvent.ToDo = (info) =>
            {
                room.ToDo_RemoveCardFromGroup(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent UseCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseCard);
            gameEvent.ToDo = (info) => {
                room.ToDo_LoseHandCard(player, card);
                room.ToDo_UseCost(player, card.Cost);
                if (card.CardType == CardType.Unit) {
                    room.Event_UseUnitCard(info, player, card, target, pos);
                } else if (card.CardType == CardType.Magic) {
                    room.Event_UseMagicCard(info, player, card, target);
                } else if (card.CardType == CardType.Plot) {
                    room.Event_UsePlotCard(info, player, card, target);
                }
            };
            return gameEvent;
        }

        public static GameEvent UseUnitCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos) {
            GameEvent gameEvent = new GameEvent(GameEventType.UseUnitCard);
            //先占位，以免战吼期间导致友方单位死亡，位置错误
            room.ToDo_UnitStand(player, card, pos);
            gameEvent.ToDo = (info) => {
                if (card.GetArranges().Count > 0) {
                    room.Event_UnitArrange(info, card, target, player);
                }
                room.ToDo_UnitInFight(info, player, card);
            };
            return gameEvent;
        }
        
        public static GameEvent UseMagicCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.UseMagicCard);
            gameEvent.ToDo = (info) => {
                room.Event_MagicTakesEffect(info, player, card, target);
            };
            return gameEvent;
        }
        
        public static GameEvent UsePlotCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.UsePlotCard);
            gameEvent.ToDo = (info) => {
                room.Event_PlotTakesEffect(info, player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UnitArrange(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitArrange);
            gameEvent.ToDo = (info) => {
                room.ToDo_UnitArrange(info, card, target, player);
            };
            return gameEvent;
        }
        
        public static GameEvent MagicTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.MagicTakesEffect);
            gameEvent.ToDo = (info) => {
                room.ToDo_MagicTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }
        
        public static GameEvent PlotTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.PlotTakesEffect);
            gameEvent.ToDo = (info) => {
                room.ToDo_PlotTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent HeroDamage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageHero);
            gameEvent.ToDo = (info) => {
                room.ToDo_HeroDamage(info, card, target, num);
            };
            return gameEvent;
        }
        
        public static GameEvent UnitBeCalled(RoomEventTypeComponent room, RoomCard card, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitBeCalled);
            //仅仅告知监听器，有单位被召唤了，没有ToDo
            return gameEvent;
        }

        public static GameEvent AttackTo(RoomEventTypeComponent room, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackTo);
            gameEvent.ToDo = (info) => {
                room.ToDo_AttackTo(info, card, target);
            };
            return gameEvent;
        }

        //对目标角色造成伤害
        public static GameEvent Damage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.Damage);
            gameEvent.ToDo = (info) => {
                if (target.CardType == CardType.Hero) { 
                    room.ToDo_HeroDamage(info, card, target, num);
                } else if (target.CardType == CardType.Unit) {
                    room.ToDo_UnitDamage(info, card, target, num);
                } else if (target.CardType == CardType.Agent) {
                    room.ToDo_AgentDamage(info, card, target, num);
                }
            };
            return gameEvent;
        }

        public static GameEvent Desecrate(RoomEventTypeComponent room, RoomCard card, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.Desecrate);
            return gameEvent;
        }

        public static GameEvent AttackOver(RoomEventTypeComponent room, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackOver);
            gameEvent.Id1 = card.Id;
            gameEvent.Id2 = target.Id;
            return gameEvent;
        }

        public static GameEvent DeadOver(RoomEventTypeComponent room, RoomCard card) {
            GameEvent gameEvent = new GameEvent(GameEventType.DeadOver);
            gameEvent.ToDo = (info) => {
                //触发阵亡特效
                room.ToDo_Fall(info, card);
            };
            return gameEvent;
        }

        public static GameEvent Dead(RoomEventTypeComponent room, List<RoomCard> cards) {
            GameEvent gameEvent = new GameEvent(GameEventType.Dead);
            gameEvent.ToDo = (info) => {
                room.ToDo_UnitDead(info, cards);
            };
            return gameEvent;
        }
        
        public static GameEvent TurnStart(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.TurnStart);
            gameEvent.Id1 = player.Id;
            gameEvent.ToDo = (info) => {
                room.ToDo_TurnStart(player);
            };
            return gameEvent;
        }
    }
}