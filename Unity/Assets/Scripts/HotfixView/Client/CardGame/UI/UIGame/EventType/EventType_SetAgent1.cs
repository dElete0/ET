namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_SetAgent1 : AEvent<Scene, SetAgent1>
    {
        protected override async ETTask Run(Scene scene, SetAgent1 args) {
            await UICGGameHelper.Room2C_NewHero(scene.GetComponent<Room>(), args.Agent);
        }
    }
}