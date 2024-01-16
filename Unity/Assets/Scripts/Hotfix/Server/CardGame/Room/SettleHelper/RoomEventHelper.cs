namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static class RoomEventHelper
    {

        //回合结束
        public static void Event_TurnOver(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player)
        {
            roomEventTypeComponent.CountClear();
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.TurnStart(roomEventTypeComponent, player.GetNextRoomPlayer()), eventInfo);
        }

        public static void Event_UseCard(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseCard(roomEventTypeComponent, player, card, target, pos), eventInfo);
        }

        public static void Event_AttackTo(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.AttackTo(eventTypeComponent, card, target), eventInfo);
        }

        public static void Event_UseUnitCard(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseUnitCard(eventTypeComponent, player, card, target, pos), eventInfo);
        }

        public static void Event_UseMagicCard(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UseMagicCard(eventTypeComponent, player, card, target), eventInfo);
        }
        
        public static void Event_UsePlotCard(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UsePlotCard(eventTypeComponent, player, card, target), eventInfo);
        }

        public static void Event_UnitArrange(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, RoomPlayer player)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.UnitArrange(eventTypeComponent, card, target, player), eventInfo);
        }

        public static void Event_AuraEffect(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomCard card, RoomPlayer player)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.AuraEffect(eventTypeComponent, card, player), eventInfo);
        }

        public static void Event_MagicTakesEffect(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.MagicTakesEffect(eventTypeComponent, card, target, player), eventInfo);
        }
        
        public static void Event_PlotTakesEffect(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomPlayer player, RoomCard card, RoomCard target)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.PlotTakesEffect(eventTypeComponent, card, target, player), eventInfo);
        }

        public static void Event_HeroDamage(this RoomEventTypeComponent eventTypeComponent, EventInfo eventInfo, RoomCard card, RoomCard target, int num)
        {
            eventTypeComponent.BroadAndSettleEvent(GameEventFactory.HeroDamage(eventTypeComponent, card, target, num), eventInfo);
        }
    }
}