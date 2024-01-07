namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class AppStartInitFinish_CreateUICGLogin: AEvent<Scene, AppStartInitFinish>
    {
        protected override async ETTask Run(Scene root, AppStartInitFinish args)
        {
            await UIHelper.Create(root, UIType.UICGLogin, UILayer.Mid);
        }
    }
}