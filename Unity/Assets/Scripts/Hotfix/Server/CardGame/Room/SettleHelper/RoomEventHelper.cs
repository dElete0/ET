namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static class RoomEventHelper
    {

        //回合结束
        public static async ETTask Event_TurnOver(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player)
        {
            roomEventTypeComponent.CountClear();
            await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.TurnStart(roomEventTypeComponent, player.GetNextRoomPlayer()), new EventInfo(0));
        }

        public static async ETTask Event_UseCard(this RoomEventTypeComponent roomEventTypeComponent, RoomPlayer player, RoomCard card, RoomCard target, int pos)
        {
            roomEventTypeComponent.CountClear();
            await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.UseCard(roomEventTypeComponent, player, card, target, pos), new EventInfo(0));
        }

        public static async ETTask Event_AttackTo(this RoomEventTypeComponent eventTypeComponent, RoomCard card, RoomCard target)
        {
            eventTypeComponent.CountClear();
            await eventTypeComponent.SettleEventWithLock(GameEventFactory.AttackTo(eventTypeComponent, card, target), new EventInfo(0));
        }
    }
}