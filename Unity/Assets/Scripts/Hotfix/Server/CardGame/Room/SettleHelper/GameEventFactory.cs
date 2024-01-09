namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEventFactory
    {
        public static GameEvent GetHandCardsFromGroup(RoomEventTypeComponent room, RoomPlayer player, int count)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCardFromGroup);
            gameEvent.ToDo = @event =>
            {
                room.ToDo_GetHandCardsFromGroup(player, count);
            };
            return gameEvent;
        }

        public static GameEvent GetCostTotal(RoomEventTypeComponent room, RoomPlayer player, int count) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetCostTotal);
            gameEvent.ToDo = @event => {
                room.ToDo_GetCostTotal(player, count);
            };
            return gameEvent;
        }

        public static GameEvent ResetCost(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.ResetCost);
            gameEvent.ToDo = @event => {
                room.ToDo_ResetCost(player);
            };
            return gameEvent;
        }

        public static GameEvent GetBaseColor(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.GetBaseColor);
            gameEvent.ToDo = @event => {
                room.ToDo_GetBaseColor(player);
            };
            return gameEvent;
        }

        public static GameEvent GetHandCard(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.GetHandCard);
            gameEvent.ToDo = @event =>
            {
                room.ToDo_GetHandCard(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent RemoveCardFromGroup(RoomEventTypeComponent room, RoomPlayer player, long cardID)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.RemoveCardFromGroup);
            gameEvent.ToDo = @event =>
            {
                room.ToDo_RemoveCardFromGroup(player, cardID);
            };
            return gameEvent;
        }

        public static GameEvent UseCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            GameEvent gameEvent = new GameEvent(GameEventType.UseCard);
            gameEvent.ToDo = @event => {
                room.ToDo_LoseHandCard(player, card);
                room.ToDo_UseCost(player, card.Cost);
                if (card.CardType == CardType.Unit) {
                    room.Event_UseUnitCard(player, card, target, pos);
                } else if (card.CardType == CardType.Magic) {
                    room.Event_UseMagicCard(player, card, target);
                }
            };
            return gameEvent;
        }

        public static GameEvent UseUnitCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target, int pos) {
            GameEvent gameEvent = new GameEvent(GameEventType.UseUnitCard);
            gameEvent.ToDo = @event => {
                if (card.GetArranges().Count > 0) {
                    room.Event_UnitArrange(card, target, player);
                }
                room.Event_CallUnit(player, card, pos);
            };
            return gameEvent;
        }
        
        public static GameEvent UseMagicCard(RoomEventTypeComponent room, RoomPlayer player, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.UseUnitCard);
            gameEvent.ToDo = @event => {
                room.Event_MagicTakesEffect(player, card, target);
            };
            return gameEvent;
        }

        public static GameEvent UnitArrange(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitArrange);
            gameEvent.ToDo = @event => {
                room.ToDo_UnitArrange(card, target, player);
            };
            return gameEvent;
        }
        
        public static GameEvent MagicTakesEffect(RoomEventTypeComponent room, RoomCard card, RoomCard target, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.UnitArrange);
            gameEvent.ToDo = @event => {
                room.ToDo_MagicTakesEffect(card, target, player);
            };
            return gameEvent;
        }

        public static GameEvent HeroDamage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.DamageHero);
            gameEvent.ToDo = @event => {
                room.ToDo_HeroDamage(card, target, num);
            };
            return gameEvent;
        }

        public static GameEvent CallUnit(RoomEventTypeComponent room, RoomCard card, RoomPlayer player, int pos) {
            GameEvent gameEvent = new GameEvent(GameEventType.CallUnit);
            gameEvent.ToDo = @event => {
                room.ToDo_CallUnit(card, player, pos);
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
            gameEvent.ToDo = @event => {
                room.ToDo_AttackTo(card, target);
            };
            return gameEvent;
        }

        //对目标角色造成伤害
        public static GameEvent Damage(RoomEventTypeComponent room, RoomCard card, RoomCard target, int num) {
            GameEvent gameEvent = new GameEvent(GameEventType.Damage);
            gameEvent.ToDo = @event => {
                if (target.CardType == CardType.Hero) { 
                    room.ToDo_HeroDamage(card, target, num);
                } else if (target.CardType == CardType.Unit) {
                    room.ToDo_UnitDamage(card, target, num);
                } else if (target.CardType == CardType.Agent) {
                    room.ToDo_UnitDamage(card, target, num);
                }
            };
            return gameEvent;
        }

        public static GameEvent AttackOver(RoomEventTypeComponent room, RoomCard card, RoomCard target) {
            GameEvent gameEvent = new GameEvent(GameEventType.AttackOver);
            return gameEvent;
        }

        public static GameEvent Dead(RoomEventTypeComponent room, RoomCard card) {
            GameEvent gameEvent = new GameEvent(GameEventType.Dead);
            gameEvent.ToDo = @event => {
                room.ToDo_Dead(card);
            };
            return gameEvent;
        }
        
        public static GameEvent TurnStart(RoomEventTypeComponent room, RoomPlayer player) {
            GameEvent gameEvent = new GameEvent(GameEventType.TurnStart);
            gameEvent.Id1 = player.Id;
            gameEvent.ToDo = @event => {
                room.ToDo_TurnStart(player);
            };
            return gameEvent;
        }
    }
}