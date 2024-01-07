namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_GetHandCardFromGroup : AEvent<Scene, GetHandCardFromGroup>
    {
        protected override async ETTask Run(Scene scene, GetHandCardFromGroup args) {
            await UICGGameHelper.Room2C_GetHandCardFromGroup(scene.GetComponent<Room>(), args.Card);
        }
    }
}