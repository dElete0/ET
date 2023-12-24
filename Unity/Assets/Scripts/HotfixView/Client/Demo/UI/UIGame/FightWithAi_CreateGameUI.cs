namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class FightWithAi_CreateGameUI : AEvent<Scene, FightWithAi>
    {
        protected override async ETTask Run(Scene scene, FightWithAi args)
        {
            //向服务器发送战斗请求
            await FightWithAiHelper.FightWithAi(scene);

            // 等待场景切换完成
            await scene.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();
            
            EventSystem.Instance.Publish(scene, new EnterMapFinish());
        }
    }
}