using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static class GameEvent_Silent
    {
        public static async ETTask ToDo_SilentTarget(this RoomEventTypeComponent room, EventInfo eventInfo, RoomCard actor, RoomCard target)
        {
            Log.Warning($"沉默{target.Name}");
            List<Power_Struct> powers = target.GetAura();
            if (powers.Count > 0)
            {
                Log.Warning($"沉默{actor.Name}的光环效果");
                await room.ToDo_AuraUnEffect(eventInfo, target);
            }
            target.AttributePowers.Clear();
            target.OtherPowers.Clear();

            RoomPlayer player = actor.GetOwner();
            CardGameComponent_Cards cardGameComponentCards = room.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            CardGameComponent_Player cards = player.GetComponent<CardGameComponent_Player>();
            List<RoomCardInfo> cardInfos = cards.RoomCardList2UnitInfoList(cardGameComponentCards);

            Room2C_FlashUnits myMessage = new Room2C_FlashUnits() { Units = cardInfos };
            RoomMessageHelper.BroadCast(room.GetParent<Room>(), myMessage);

            target.GetComponent<CardEventTypeComponent>().UnitGameEventTypes.Clear();
        }
    }
}