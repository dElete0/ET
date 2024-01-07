namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_GetHandCardsFromGroup : AEvent<Scene, GetHandCardsFromGroup>
    {
        protected override async ETTask Run(Scene scene, GetHandCardsFromGroup args) {
            await UICGGameHelper.Room2C_GetHandCardsFromGroup(scene.GetComponent<Room>(), args.Cards);
        }
    }
}