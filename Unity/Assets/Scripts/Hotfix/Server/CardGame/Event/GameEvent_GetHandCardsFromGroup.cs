namespace ET.Server {
    [Event(SceneType.Map)]
    [FriendOfAttribute(typeof(ET.Component_Player_Group))]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    [FriendOfAttribute(typeof(ET.GameCard))]
    public class GameEvent_GetHandCardsFromGroup : AEvent<Scene, GameEventType_GetHandCardsFromGroup>
    {
        protected override async ETTask Run(Scene scene, GameEventType_GetHandCardsFromGroup eventType)
        {
            Log.Debug(eventType.Cards.Parent.ToString() + "抽卡");
            Component_Player_HandCards handCards = eventType.Cards.GetComponent<Component_Player_HandCards>();
            Log.Warning(handCards.ToString());
            Component_Player_Group group = eventType.Cards.GetComponent<Component_Player_Group>();

            for (int i = 0; i < eventType.Count; i++)
            {
                GameCard card = null;
                if (group.cards != null && group.cards.Count > 0)
                {
                    card = group.cards[0];
                }

                if (card == null) return;
                Log.Debug("抽卡:" + i + "/" + eventType.Count);
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_RemoveCardFromGroup() { Card = card, Group = group });
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_GetHandCard() { Card = card, HandCards = handCards });
                GamePlayer client = eventType.Cards.GetParent<GamePlayer>();
                Log.Warning(client.unit.ToString());
                client.unit.M2C_GetHandCardsFromGroup(0, new CardInfo() 
                {
                    Type = (int)card.CardType,
                    Attack = card.Attack, 
                    HP = card.HP,
                });
            }

            eventType.Room.GetComponent<ObjectWait>().Notify(new Wait_GameEventType_GetHandCardsFromGroup() { Error = WaitTypeError.Success });
            await ETTask.CompletedTask;
        }
    }
}