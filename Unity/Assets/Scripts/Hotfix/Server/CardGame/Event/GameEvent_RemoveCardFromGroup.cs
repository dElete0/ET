namespace ET.Server {
    [Event(SceneType.Map)]
    [FriendOfAttribute(typeof(ET.Component_Player_Group))]
    public class GameEvent_RemoveCard : AEvent<Scene, GameEventType_RemoveCardFromGroup>
    {
        protected override async ETTask Run(Scene scene, GameEventType_RemoveCardFromGroup eventType)
        {
            eventType.Group.cards.Remove(eventType.Card);
            await ETTask.CompletedTask;
        }
    }
}