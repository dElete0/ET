namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Match2C_NotifyGameMatchSuccessHandler: MessageHandler<Scene, Match2C_NotifyGameMatchSuccess>
    {
        protected override async ETTask Run(Scene root, Match2C_NotifyGameMatchSuccess message)
        {
            Log.Warning("收到了服务器匹配成功的消息");
            await CGSceneChangeHelper.SceneChangeTo(root, "Map1", message.ActorId.InstanceId);
        }
    }
}