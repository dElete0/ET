namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_OperateFailHandler : MessageHandler<Scene, Room2C_OperateFail>
    {
        protected override async ETTask Run(Scene root, Room2C_OperateFail message) {
            Log.Error("Client收到报错：" + ((Room2C_OperateFailType)message.FailId).ToString());
            await ETTask.CompletedTask;
            //await EventSystem.Instance.PublishAsync(root, new NewHero() {Hero = message.Hero});
        }
    }
}