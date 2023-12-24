namespace ET.Client
{
	[Event(SceneType.Demo)]
	public class LoginFinish_CreateMainUI: AEvent<Scene, LoginFinish>
	{
		protected override async ETTask Run(Scene scene, LoginFinish args)
		{
			await UIHelper.Create(scene, UIType.UIMain, UILayer.Mid);
		}
	}
}
