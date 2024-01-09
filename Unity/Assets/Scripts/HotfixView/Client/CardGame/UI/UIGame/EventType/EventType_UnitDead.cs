namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_UnitDead : AEvent<Scene, UnitDead>
    {
        protected override async ETTask Run(Scene scene, UnitDead args)
        {
            await UICGGameHelper.UnitDead(scene.GetComponent<Room>(), args.CardId);
        }
    }
}