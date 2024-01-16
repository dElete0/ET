namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_GetHandCards : AEvent<Scene, GetHandCards>
    {
        protected override async ETTask Run(Scene scene, GetHandCards args) {
            await UICGGameHelper.Room2C_GetHandCards(scene.GetComponent<Room>(), args.Cards);
        }
    }
}