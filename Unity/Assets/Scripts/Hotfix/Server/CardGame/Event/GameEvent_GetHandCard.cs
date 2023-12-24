namespace ET.Server {
    [Event(SceneType.Map)]
    [FriendOfAttribute(typeof(ET.Component_Player_HandCards))]
    public class GameEvent_GetHandCard : AEvent<Scene, GameEventType_GetHandCard>
    {
        protected override async ETTask Run(Scene scene, GameEventType_GetHandCard eventType)
        {
            if (eventType.HandCards.HandCards.Count >= eventType.HandCards.CountMax)
            {
                // Todo 爆牌

            }
            else
            {
                eventType.HandCards.HandCards.Add(eventType.Card);
            }
            await ETTask.CompletedTask;
        }
    }
}