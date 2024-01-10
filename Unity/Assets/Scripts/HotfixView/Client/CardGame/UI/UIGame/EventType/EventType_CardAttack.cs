namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_CardAttack : AEvent<Scene, RoomCardAttack>
    {
        protected override async ETTask Run(Scene scene, RoomCardAttack args) {
            await UICGGameHelper.Room2C_RoomCardAttack(scene.GetComponent<Room>(), args.ActorId, args.TargetId);
        }
    }
}