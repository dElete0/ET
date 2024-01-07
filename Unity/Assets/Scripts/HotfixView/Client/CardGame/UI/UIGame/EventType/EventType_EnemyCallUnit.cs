namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyCallUnit : AEvent<Scene, EnemyCallUnit>
    {
        protected override async ETTask Run(Scene scene, EnemyCallUnit args) {
            await UICGGameHelper.Room2C_CallUnit(scene.GetComponent<Room>(), args.Card, false);
        }
    }
}