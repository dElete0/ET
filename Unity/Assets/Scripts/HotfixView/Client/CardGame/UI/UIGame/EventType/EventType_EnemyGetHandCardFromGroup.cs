namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyGetHandCardFromGroup : AEvent<Scene, EnemyGetHandCardFromGroup>
    {
        protected override async ETTask Run(Scene scene, EnemyGetHandCardFromGroup args) {
            await UICGGameHelper.Room2C_EnemyGetHandCardFromGroup(scene.GetComponent<Room>(), args.Card);
        }
    }
}