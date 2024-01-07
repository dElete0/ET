namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_NewHero : AEvent<Scene, NewHero>
    {
        protected override async ETTask Run(Scene scene, NewHero args) {
            await UICGGameHelper.Room2C_NewHero(scene.GetComponent<Room>(), args.Hero);
        }
    }
}