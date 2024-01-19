namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_AttackHandler: MessageHandler<Scene, Room2C_Attack>
    {
        protected override async ETTask Run(Scene root, Room2C_Attack message)
        {
            await EventSystem.Instance.PublishAsync(root, new RoomCardAttack() {ActorId = message.Actor, TargetId = message.Target});
        }
    }
}