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
                PowerType = Power_Type.PowerToUseBaseCard,
                TriggerPowerType = TriggerPowerType.Release,
                Count1 = baseId
            }, actor, null, null, player));
        }
    }

    public static async ETTask ToDo_PowerToUseCard(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, RoomCard card) {
        CardGameComponent_Cards cards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
        RoomPlayer player = actor.GetOwner();
        CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
        if (card.CardType == CardType.Unit) {
            await roomEventTypeComponent.ToDo_CallUnitCard(eventInfo, player, card, playerInfo.Units.Count);
        } else if (card.CardType == CardType.Magic || card.CardType == CardType.Plot) {
            RoomCard target = null;
            switch (card.UseCardType) {
                case UseCardType.NoTarget:
                    break;
                case UseCardType.ToActor:
                    target = player.GetRandomActor(cards);
                    break;
                case UseCardType.ToMyActor:
                    target = player.GetRandomMyActor(cards);
                    break;
                case UseCardType.ToEnemyActor:
                    target = player.GetRandomEnemyActor(cards);
                    break;
                case UseCardType.ToUnit:
                    target = player.GetRandomUnit(cards);
                    break;
                case UseCardType.ToMyUnit:
                    target = player.GetRandomMyUnit(cards);
                    break;
                case UseCardType.ToEnemyUnit:
                    target = player.GetRandomEnemyUnit(cards);
                    break;
                case UseCardType.ToAgent:
                    target = player.GetRandomAgent(cards);
                    break;
                case UseCardType.ToMyAgent:
                    target = player.GetRandomMyAgent(cards);
                    break;
                case UseCardType.ToEnemyAgent:
                    target = player.GetRandomEnemyAgent(cards);
                    break;
                case UseCardType.ToHero:
                    target = player.GetRandomHero(cards);
                    break;
            }

            if (card.CardType == CardType.Magic) {
                await roomEventTypeComponent.ToDo_UseMagicCard(eventInfo, player, card, target);
            } else {
                
                await roomEventTypeComponent.ToDo_PlotTakesEffect(eventInfo, player, card, target);
            }
        }
    }

    public static async ETTask ToDo_PowerToUseBaseCard(this RoomEventTypeComponent roomEventTypeComponent, EventInfo eventInfo, RoomCard actor, int baseId) {
        CardGameComponent_Cards cards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
        RoomCard cardByBaseId = cards.AddChild<RoomCard, int, long>(baseId, actor.PlayerId);
        await ToDo_PowerToUseCard(roomEventTypeComponent, eventInfo, actor, cardByBaseId);
    }
}