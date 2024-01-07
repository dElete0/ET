namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CardGetDamage : AEvent<Scene, CardGetDamage>
    {
        protected override async ETTask Run(Scene scene, CardGetDamage args) {
            await UICGGameHelper.Room2C_CardGetDamage(scene.GetComponent<Room>(), args.Card, args.Hurt);
        }
    }
}