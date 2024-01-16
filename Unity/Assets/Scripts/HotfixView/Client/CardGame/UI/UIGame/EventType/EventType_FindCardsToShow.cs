namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_FindCardsToShow : AEvent<Scene, FindCardsToShow>
    {
        protected override async ETTask Run(Scene scene, FindCardsToShow args) {
            await UICGGameHelper.Room2C_FindCardsToShow(scene.GetComponent<Room>(), args.Cards);
        }
    }
}