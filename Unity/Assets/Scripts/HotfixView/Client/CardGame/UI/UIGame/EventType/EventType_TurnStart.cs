namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_TurnStart : AEvent<Scene, TurnStart>
    {
        protected override async ETTask Run(Scene scene, TurnStart args)
        {
            await UICGGameHelper.TurnStart(scene.GetComponent<Room>(), args);
        }
    }
}