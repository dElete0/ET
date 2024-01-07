namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_GetColor : AEvent<Scene, GetColor>
    {
        protected override async ETTask Run(Scene scene, GetColor args) {
            await UICGGameHelper.Room2C_GetColor(scene.GetComponent<Room>(), args.Color, args.Num, args.IsMy);
        }
    }
}