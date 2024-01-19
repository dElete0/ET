namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_RiskSuccessHandler : MessageHandler<Scene, Room2C_RiskSuccess>
    {
        protected override async ETTask Run(Scene root, Room2C_RiskSuccess message) {
            await EventSystem.Instance.PublishAsync(root, new RiskSuccess() {Card = message.Card, IsSuccess = message.IsRiskSuccess});
        }
    }
}