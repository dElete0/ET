namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyGetHandCardsFromGroup : AEvent<Scene, EnemyGetHandCardsFromGroup>
    {
        protected override async ETTask Run(Scene scene, EnemyGetHandCardsFromGroup args) {
            await UICGGameHelper.Room2C_EnemyGetHandCardsFromGroup(scene.GetComponent<Room>(), args.Cards);
        }
    }
}