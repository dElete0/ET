using System;

namespace ET.Client {

    public static class FightWithAiHelper {
        public static async ETTask FightWithAi(Scene root)
        {
            try
            {
                G2C_FightWithAi response = await root.GetComponent<ClientSenderCompnent>().Call(new C2G_FightWithAi()) as G2C_FightWithAi;
                
                // 等待场景切换完成
                //await root.GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();
                
                //EventSystem.Instance.Publish(root, new EnterMapFinish());
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}