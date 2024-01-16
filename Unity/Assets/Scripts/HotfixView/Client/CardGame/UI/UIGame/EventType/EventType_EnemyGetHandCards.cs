namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyGetHandCards : AEvent<Scene, EnemyGetHandCards>
    {
        protected override async ETTask Run(Scene scene, EnemyGetHandCards args) {
            await UICGGameHelper.Room2C_EnemyGetHandCards(scene.GetComponent<Room>(), args.Cards);
        }
    }
}