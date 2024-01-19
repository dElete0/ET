namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_TreatTergetsHandler : MessageHandler<Scene, Room2C_TreatTergets>
    {
        protected override async ETTask Run(Scene root, Room2C_TreatTergets message) {
            await EventSystem.Instance.PublishAsync(root, new TreatTergets() {CardInfos = message.Cards, Nums = message.Nums});
        }
    }
}