namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_NewAgentHandler: MessageHandler<Scene, Room2C_NewAgent> {
        protected override async ETTask Run(Scene root, Room2C_NewAgent message) {
            await EventSystem.Instance.PublishAsync(root, new NewAgentType() {Agent1 = message.Agent1, Agent2 = message.Agent2});
        }
    }
}