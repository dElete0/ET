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
                return room.ToDo_GetHandCardsFromGroup(info, player, count);
            };
            return gameEvent;
        }

        public static GameEvent GetCostTotal(RoomEventTypeComponent room, RoomPlayer player, int count)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCostTotal);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_GetCostTotal(player, count);
            };
            return gameEvent;
        }

        public static GameEvent ResetCost(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.ResetCost);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_ResetCost(player);
            };
            return gameEvent;
        }

        public static GameEvent GetBaseColor(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetBaseColor);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_GetBaseColor(player);
            };
            return gameEvent;
        }

        public static GameEvent AllAttackCountClear(RoomEventTypeComponent room, PlayerEventTypeComponent playerEventType, CardGameComponent_Player cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AllAttackCountClear);
            CardGameComponent_Cards cardGameComponent = cards.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            
            gameEvent.ToDo = (ge, info) => {
                return cardGameComponent.ToDo_AllAttackCountClear(cards);
            };
            return gameEvent;
        }

        public static GameEvent AttackCountClear(RoomEventTypeComponent room, RoomCard card)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackCountClear);
            gameEvent.ToDo = (ge, info) => {
                return card.ToDo_AttackCountClear();
            };
            return gameEvent;
        }
        
        public static GameEvent GetHandCards(RoomEventTypeComponent room, RoomPlayer player, List<RoomCard> cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_GetHandCards(player, cards);
            };
            return gameEvent;
        }

        public static GameEvent GetHandCardFromShowCard(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_GetHandCardFromShowCard(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent RemoveCardFromGroup(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveCardFromGroup);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_RemoveCardFromGroup(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent UseCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseCard);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_UseCard(info, player, card, target, pos);
            };
            return gameEvent;
        }

        public static GameEvent UseUnitCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseUnitCard);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_UseUnitCard(info, player, card, target, pos);
            };
            return gameEvent;
        }

        public static GameEvent UseMagicCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseMagicCard);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_UseMagicCard(info, player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UsePlotCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UsePlotCard);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_PlotTakesEffect(info, player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UnitArrange(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitArrange);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_UnitArrange(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent AuraEffect(RoomEventTypeComponent room, RoomCard card, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.AuraEffect);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AuraEffect(info, card, player);
            };
            return gameEvent;
        }

        public static GameEvent AuraEffectToTarget(RoomEventTypeComponent room, RoomCard card) {
            GameEvent gameEvent = new GameEvent(GameEventType.AuraEffectToTarget);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AuraEffectToTarget(info, card, ge);
            };
            return gameEvent;
        }

        public static GameEvent MagicTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.MagicTakesEffect);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_MagicTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent PlotTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.PlotTakesEffect);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_PlotTakesEffect(info, card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent HeroDamage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageHero);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_HeroDamage(info, card, target, num);
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
                return room.ToDo_AttackTo(info, card, target);
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
                    return room.ToDo_HeroDamage(info, card, target, num);
                }
                else if (target.CardType == CardType.Unit)
                {
                    return room.ToDo_UnitDamage(info, card, target, num);
                }
                else if (target.CardType == CardType.Agent)
                {
                    return room.ToDo_AgentDamage(info, card, target, num);
                }

                return null;
            };
            return gameEvent;
        }

        public static GameEvent Desecrate(RoomEventTypeComponent room, RoomCard card, int num, int loopCount)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.Desecrate);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_Desecrate(info, card, num, loopCount);
            };
            return gameEvent;
        }

        public static GameEvent DamageAllUnit(RoomEventTypeComponent room, RoomCard card, RoomPlayer player, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageAllUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_DamageAllUnit(info, card, player, num);
            };
            return gameEvent;
        }

        public static GameEvent CallTargetUnit(RoomEventTypeComponent room, RoomPlayer player, RoomCard actor, int cardId, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_CalltargetUnit(info, player, actor, cardId, num, CallType.Nomal);
            };
            return gameEvent;
        }
        
        public static GameEvent GetQualifications(RoomEventTypeComponent room, RoomCard actor, int color, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetQualifications);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_GetColor(actor, color, num);
            };
            return gameEvent;
        }

        public static GameEvent GetArmor(RoomEventTypeComponent room, RoomCard actor, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetArmor);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_GetArmor(actor, num);
            };
            return gameEvent;
        }

        public static GameEvent TargetGetPower(RoomEventTypeComponent room, RoomCard actor, RoomCard target, Power_Type power) {
            GameEvent gameEvent = new GameEvent(GameEventType.TargetGetPower);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_TargetGetPower(actor, target, power);
            };
            return gameEvent;
        }

        public static GameEvent SilentTarget(RoomEventTypeComponent room, RoomCard actor, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.SilentTarget);
            gameEvent.Actor = actor.Id;
            gameEvent.Target = actor.Id;
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_SilentTarget(info, actor, target);
            };
            return gameEvent;
        }

        public static GameEvent AttributeAuraEffect(RoomEventTypeComponent room, RoomCard actor, Power_Struct power) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraEffect);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AttributeAuraEffect(info, actor, power);
            };
            return gameEvent;
        }

        public static GameEvent CallRedDragon(RoomEventTypeComponent room, RoomPlayer player, RoomCard actor, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_CalltargetUnit(info, player, actor, 3000013, num, CallType.RedDragon);
            };
            return gameEvent;
        }

        public static GameEvent KillTargetUnit(RoomEventTypeComponent room, RoomCard actor, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.KillTargetUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_KillTargetUnit(info, actor, target);
            };
            return gameEvent;
        }
        
        public static GameEvent RemoveTargetUnit(RoomEventTypeComponent room, RoomCard actor, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveTargetUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_RemoveTargetUnit(info, actor, target);
            };
            return gameEvent;
        }
        
        public static GameEvent AddCardToGroupShow(RoomEventTypeComponent room, RoomCard actor, int baseId, int num, int att) {
            GameEvent gameEvent = new GameEvent(GameEventType.AddCardToGroup);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AddCardToGroup(info, actor, baseId, num, att, true);
            };
            return gameEvent;
        }
        
        public static GameEvent AddCardToGroupHide(RoomEventTypeComponent room, RoomCard actor, int baseId, int num, int att) {
            GameEvent gameEvent = new GameEvent(GameEventType.AddCardToGroup);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AddCardToGroup(info, actor, baseId, num, att, false);
            };
            return gameEvent;
        }

        public static GameEvent SwapArmor(RoomEventTypeComponent room, RoomCard actor) {
            GameEvent gameEvent = new GameEvent(GameEventType.SwapArmor);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_SwapArmor(actor);
            };
            return gameEvent;
        }
        
        public static GameEvent TreatTarget(RoomEventTypeComponent room, RoomCard actor, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.TreatTarget);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_TreatTargets(actor, new List<long>(){target.Id}, num);
            };
            return gameEvent;
        }
        
        public static GameEvent TargetGetAttribute(RoomEventTypeComponent room, RoomCard actor, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.TargetGetAttribute);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_TargetGetAttribute(actor, target, num);
            };
            return gameEvent;
        }

        public static GameEvent GoldenShip(RoomEventTypeComponent room, RoomCard actor) {
            GameEvent gameEvent = new GameEvent(GameEventType.GoldenShip);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_GoldenShip(info, actor);
            };
            return gameEvent;
        }

        public static GameEvent KillAllUnit(RoomEventTypeComponent room, RoomCard actor) {
            GameEvent gameEvent = new GameEvent(GameEventType.KillAllUnit);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDO_KillAllUnit(info, actor);
            };
            return gameEvent;
        }

        public static GameEvent FindAndCloneCard(RoomEventTypeComponent room, RoomCard actor, int num, int type) {
            GameEvent gameEvent = new GameEvent(GameEventType.FindAndCloneCard);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_FindAndCloneCard(info, actor, num, type);
            };
            return gameEvent;
        }
        
        public static GameEvent AttributeAuraUnEffect(RoomEventTypeComponent room, RoomCard actor, Power_Struct power) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraUnEffect);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AttributeAuraUnEffect(info, actor, power);
            };
            return gameEvent;
        }

        public static GameEvent AttributeAuraEffectToTarget(RoomEventTypeComponent room, RoomCard actor, GameEvent trigger, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttributeAuraEffectToTarget);
            gameEvent.ToDo = (ge, info) => {
                return room.ToDo_AttributeAuraEffectToTarget(info, actor, trigger, num);
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
                return room.ToDo_Fall(info, card);
            };
            return gameEvent;
        }

        public static GameEvent Dead(RoomEventTypeComponent room, List<RoomCard> cards)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.Dead);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_UnitsDead(info, cards);
            };
            return gameEvent;
        }

        public static GameEvent Remove(RoomEventTypeComponent roomEventTypeComponent, List<RoomCard> cards) {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveUnits);
            gameEvent.ToDo = (ge, info) =>
            {
                return roomEventTypeComponent.ToDo_RemoveUnits(info, cards);
            };
            return gameEvent;
        }

        public static GameEvent TurnStart(RoomEventTypeComponent room, RoomPlayer player)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.TurnStart);
            gameEvent.Player = player.Id;
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_TurnStart(player);
            };
            return gameEvent;
        }

        public static GameEvent PowerToUseCard(RoomEventTypeComponent room, RoomCard actor, int baseId) {
            GameEvent gameEvent = new GameEvent(GameEventType.PowerToUseCard);
            gameEvent.ToDo = (ge, info) =>
            {
                return room.ToDo_PowerToUseCard(info, actor, baseId);
            };
            return gameEvent;
        }
    }
}