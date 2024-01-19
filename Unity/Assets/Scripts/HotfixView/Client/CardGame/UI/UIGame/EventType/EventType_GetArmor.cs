namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_GetArmor : AEvent<Scene, GetArmor>
    {
        protected override async ETTask Run(Scene scene, GetArmor args) {
            await UICGGameHelper.GetArmor(scene.GetComponent<Room>(), args.Num, args.IsMy);
        }
    }
}