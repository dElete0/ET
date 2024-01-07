namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class LoginFinish_CreateUICGLobby : AEvent<Scene, LoginFinish>
    {
        protected override async ETTask Run(Scene scene, LoginFinish args)
        {
            await UIHelper.Remove(scene, UIType.UICGLogin);
            await UIHelper.Create(scene, UIType.UICGLobby, UILayer.Mid);
        }
    }
}