namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyNewAgentHandler: MessageHandler<Scene, Room2C_EnemyNewAgent> {
        protected override async ETTask Run(Scene root, Room2C_EnemyNewAgent message) {
            await EventSystem.Instance.PublishAsync(root, new EnemyNewAgentType() {Agent1 = message.Agent1, Agent2 = message.Agent2});
        }
    }
}