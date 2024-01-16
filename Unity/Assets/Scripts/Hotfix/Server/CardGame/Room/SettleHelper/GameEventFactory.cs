using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.Server.PlayerEventTypeComponent))]
    public static class GameEventFactory
    {
        public static GameEvent GetHandCardsFromGroup(RoomEventTypeComponent room, RoomPlayer player, int count)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCardFromGroup);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_GetHandCardsFromGroup(info, player, count);
            };
            return gameEvent;
        }

        public static GameEvent GetCostTotal(RoomEventTypeComponent room, RoomPlayer player, int count)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCostTotal);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_GetCostTotal(player, count);
            };
            return gameEvent;
        }

        public static GameEvent ResetCost(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.ResetCost);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_ResetCost(player);
            };
            return gameEvent;
        }

        public static GameEvent GetBaseColor(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetBaseColor);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_GetBaseColor(player);
            };
            return gameEvent;
        }

        public static GameEvent AllAttackCountClear(RoomEventTypeComponent room, PlayerEventTypeComponent playerEventType, CardGameComponent_Player cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AllAttackCountClear);
            CardGameComponent_Cards cardGameComponent = cards.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            
            gameEvent.ToDo = (ge, info) => {
                List<long> actors = new List<long>(cards.Units);
                actors.Add(cards.Hero);
                if (cards.Agent1 != 0) actors.Add(cards.Agent1);
                if (cards.Agent2 != 0) actors.Add(cards.Agent2);
                foreach (var actor in actors) {
                    RoomCard card = cardGameComponent.GetChild<RoomCard>(actor);
                    if (card == null) Log.Error($"未获取到角色:{actor}");
                    card.AttackCount = card.AttackCountMax;
                    card.IsCallThisTurn = false;
                }
            };
            return gameEvent;
        }

        public static GameEvent AttackCountClear(RoomEventTypeComponent room, RoomCard card)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackCountClear);
            gameEvent.ToDo = (ge, info) => {
                card.AttackCount = card.AttackCountMax;
                card.IsCallThisTurn = false;
            };
            return gameEvent;
        }
        
        public static GameEvent GetHandCards(RoomEventTypeComponent room, RoomPlayer player, List<RoomCard> cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_GetHandCards(player, cards);
            };
            return gameEvent;
        }

        public static GameEvent GetHandCardFromShowCard(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_GetHandCardFromShowCard(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent RemoveCardFromGroup(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveCardFromGroup);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_RemoveCardFromGroup(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent UseCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseCard);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_LoseHandCard(player, card);
                room.ToDo_UseCost(player, card.Cost);
                if (card.CardType == CardType.Unit)
                {
                    room.Event_UseUnitCard(info, player, card, target, pos);
                }
                else if (card.CardType == CardType.Magic)
                {
                    room.Event_UseMagicCard(info, player, card, target);
                }
                else if (card.CardType == CardType.Plot)
                {
                    room.Event_UsePlotCard(info, player, card, target);
                }
            };
            return gameEvent;
        }

        public static GameEvent UseUnitCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseUnitCard);
            gameEvent.ToDo = (ge, info) =>
            {
                //先占位，以免战吼期间导致友方单位死亡，位置错误
                room.ToDo_UnitStand(player, card, pos);
                if (card.GetArranges().Count > 0)
                {
                    room.Event_UnitArrange(info, card, target, player);
                }

                if (card.GetAura().Count > 0) {
                    room.Event_AuraEffect(info, card, player);
                }
                room.ToDo_UnitInFight(info, player, card);
            };
            return gameEvent;
        }

        public static GameEvent UseMagicCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseMagicCard);
            gameEvent.ToDo = (ge, info) =>
            {
                room.Event_MagicTakesEffect(info, player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UsePlotCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UsePlotCard);
            gameEvent.ToDo = (ge, info) =>
            {
                room.Event_PlotTakesEffect(info, player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UnitArrange(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitArrange);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_UnitArrange(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent AuraEffect(RoomEventTypeComponent room, RoomCard card, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.AuraEffect);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_AuraEffect(info, card, player);
            };
            return gameEvent;
        }

        public static GameEvent AuraUnEffect(RoomEventTypeComponent room, RoomCard card) {
            GameEvent gameEvent = new GameEvent(GameEventType.AuraUnEffect);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_AuraUnEffect(info, card);
            };
            return gameEvent;
        }

        public static GameEvent AuraEffectToTarget(RoomEventTypeComponent room, RoomCard card) {
            GameEvent gameEvent = new GameEvent(GameEventType.AuraEffectToTarget);
            gameEvent.ToDo = (ge, info) => {
                CardGameComponent_Cards cards = card.GetParent<CardGameComponent_Cards>();
                RoomCard target = cards.GetChild<RoomCard>(ge.Actor);
                room.ToDo_AuraEffectToTarget(info, card, ge);
            };
            return gameEvent;
        }

        public static GameEvent MagicTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.MagicTakesEffect);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_MagicTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent PlotTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.PlotTakesEffect);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_PlotTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent HeroDamage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageHero);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_HeroDamage(info, card, target, num);
            };
            return gameEvent;
        }

        public static GameEvent CallUnitOver(RoomEventTypeComponent room, RoomCard card, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnitOver);
            gameEvent.Actor = card.Id;
            //仅仅告知监听器，有单位被召唤了，没有ToDo
            return gameEvent;
        }

        public static GameEvent AttackTo(RoomEventTypeComponent room, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackTo);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_AttackTo(info, card, target);
            };
            return gameEvent;
        }

        //对目标角色造成伤害
        public static GameEvent Damage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.Damage);
            gameEvent.ToDo = (ge, info) =>
            {
                if (target.CardType == CardType.Hero)
                {
                    room.ToDo_HeroDamage(info, card, target, num);
                }
                else if (target.CardType == CardType.Unit)
                {
                    room.ToDo_UnitDamage(info, card, target, num);
                }
                else if (target.CardType == CardType.Agent)
                {
                    room.ToDo_AgentDamage(info, card, target, num);
                }
            };
            return gameEvent;
        }

        public static GameEvent Desecrate(RoomEventTypeComponent room, RoomCard card, int num)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.Desecrate);
            return gameEvent;
        }

        public static GameEvent DamageAllUnit(RoomEventTypeComponent room, RoomCard card, RoomPlayer player, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageAllUnit);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_DamageAllUnit(info, card, player, num);
            };
            return gameEvent;
        }

        public static GameEvent CallTargetUnit(RoomEventTypeComponent room, RoomPlayer player, RoomCard actor, int cardId, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnit);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_CalltargetUnit(info, player, actor, cardId, num, CallType.Nomal);
            };
            return gameEvent;
        }

        public static GameEvent SilentTarget(RoomEventTypeComponent room, RoomCard actor, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.SilentTarget);
            gameEvent.Actor = actor.Id;
            gameEvent.Target = actor.Id;
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_SilentTarget(info, actor, target);
            };
            return gameEvent;
        }

        public static GameEvent AttributeAuraEffect(RoomEventTypeComponent room, RoomCard actor, Power_Struct power) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraEffect);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_AttributeAuraEffect(info, actor, power);
            };
            return gameEvent;
        }

        public static GameEvent CallRedDragon(RoomEventTypeComponent room, RoomPlayer player, RoomCard actor, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnit);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_CalltargetUnit(info, player, actor, 3000013, num, CallType.RedDragon);
            };
            return gameEvent;
        }

        public static GameEvent KillTargetUnit(RoomEventTypeComponent room, RoomCard actor, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.KillTargetUnit);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_KillTargetUnit(info, actor, target);
            };
            return gameEvent;
        }

        public static GameEvent KillAllUnit(RoomEventTypeComponent room, RoomCard actor) {
            GameEvent gameEvent = new GameEvent(GameEventType.KillAllUnit);
            gameEvent.ToDo = (ge, info) => {
                room.ToDO_KillAllUnit(info, actor);
            };
            return gameEvent;
        }

        public static GameEvent FindAndCloneCard(RoomEventTypeComponent room, RoomCard actor, int num, int type) {
            GameEvent gameEvent = new GameEvent(GameEventType.FindAndCloneCard);
            gameEvent.ToDo = (et, ge, info) => {
                et = room.ToDo_FindAndCloneCard(info, actor, num, type);
            };
            return gameEvent;
        }
        
        public static GameEvent AttributeAuraUnEffect(RoomEventTypeComponent room, RoomCard actor, Power_Struct power) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraUnEffect);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_AttributeAuraUnEffect(info, actor, power);
            };
            return gameEvent;
        }

        public static GameEvent AttributeAuraEffectToTarget(RoomEventTypeComponent room, RoomCard actor, GameEvent trigger, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraEffectToTarget);
            gameEvent.ToDo = (ge, info) => {
                room.ToDo_AttributeAuraEffectToTarget(info, actor, trigger, num);
            };
            return gameEvent;
        }

        public static GameEvent AttackOver(RoomEventTypeComponent room, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackOver);
            gameEvent.Actor = card.Id;
            gameEvent.Target = target.Id;
            return gameEvent;
        }

        public static GameEvent DeadOver(RoomEventTypeComponent room, RoomCard card)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.DeadOver);
            gameEvent.Actor = card.Id;
            gameEvent.ToDo = (ge, info) =>
            {
                //触发阵亡特效
                room.ToDo_Fall(info, card);
            };
            return gameEvent;
        }

        public static GameEvent Dead(RoomEventTypeComponent room, List<RoomCard> cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.Dead);
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_UnitDead(info, cards);
            };
            return gameEvent;
        }

        public static GameEvent TurnStart(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.TurnStart);
            gameEvent.Player = player.Id;
            gameEvent.ToDo = (ge, info) =>
            {
                room.ToDo_TurnStart(player);
            };
            return gameEvent;
        }
    }
}