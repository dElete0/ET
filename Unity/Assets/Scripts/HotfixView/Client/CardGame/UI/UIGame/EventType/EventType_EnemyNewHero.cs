namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_EnemyNewHero : AEvent<Scene, EnemyNewHeroType>
    {
        protected override async ETTask Run(Scene scene, EnemyNewHeroType args) {
            await UICGGameHelper.Room2C_EnemyNewHero(scene.GetComponent<Room>(), args.Hero);
        }
    }
}