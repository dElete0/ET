namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static class RoomEventHelper
    {

        //回合结束
        public static void Event_TurnOver(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player)
        {
            roomEventTypeComponent.CountClear();
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.TurnStart(roomEventTypeComponent, player.GetNextRoomPlayer()));
        }

        public static void Event_UseCard(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseCard(roomEventTypeComponent, player, card, target, pos));
        }

        public static void Event_AttackTo(this RoomEventTypeComponent eventTypeComponent, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttackTo(eventTypeComponent, card, target));
        }

        public static void Event_UseUnitCard(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseUnitCard(eventTypeComponent, player, card, target, pos));
        }

        public static void Event_UseMagicCard(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseMagicCard(eventTypeComponent, player, card, target));
        }
        
        public static void Event_UsePlotCard(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UsePlotCard(eventTypeComponent, player, card, target));
        }

        public static void Event_UnitArrange(this RoomEventTypeComponent eventTypeComponent, RoomCard card, RoomCard target, RoomPlayer player)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UnitArrange(eventTypeComponent, card, target, player));
        }

        public static void Event_MagicTakesEffect(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.MagicTakesEffect(eventTypeComponent, card, target, player));
        }
        
        public static void Event_PlotTakesEffect(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.PlotTakesEffect(eventTypeComponent, card, target, player));
        }

        public static void Event_CallUnit(this RoomEventTypeComponent eventTypeComponent, RoomPlayer player, RoomCard card, int pos)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.CallUnit(eventTypeComponent, card, player, pos));
        }

        public static void Event_HeroDamage(this RoomEventTypeComponent eventTypeComponent, RoomCard card, RoomCard target, int num)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.HeroDamage(eventTypeComponent, card, target, num));
        }
    }
}