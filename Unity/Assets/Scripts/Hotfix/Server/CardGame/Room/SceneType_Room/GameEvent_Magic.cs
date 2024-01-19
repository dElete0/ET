namespace ET.Server;
[FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
[FriendOfAttribute(typeof(ET.RoomCard))]
public static class GameEvent_Magic
{
    public static async ETTask ToDo_GoldenShip(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor)
    {
        await ETTask.CompletedTask;
        RoomPlayer player = actor.GetOwner();
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        foreach (var baseId in playerInfo.UsedMagicList)
        {
            if (baseId == 5000025)
            {
                continue;
            }
            eventInfo.PowerStructs.Add((new Power_Struct()
            {
                PowerType = Power_Type.PowerToUseCard,
                TriggerPowerType = TriggerPowerType.Release,
                Count1 = baseId
            }, actor, null, player));
        }
    }

    public static async ETTask ToDo_PowerToUseCard(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int baseId) {
        RoomPlayer player = actor.GetOwner();
        CardGameComponent_Cards cards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        RoomCard cardByBaseId = cards.AddChild<RoomCard, int, long>(baseId, actor.PlayerId);
        if (cardByBaseId.CardType == CardType.Unit) {
            await roomEventTypeComponent.ToDo_CallUnitCard(eventInfo, player, cardByBaseId, playerInfo.Units.Count);
        } else if (cardByBaseId.CardType == CardType.Magic) {
            if (actor.UseCardType == UseCardType.NoTarget) {
                await roomEventTypeComponent.ToDo_UseMagicCard(eventInfo, player, cardByBaseId, null);
            }
        }
    }
}