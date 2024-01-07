using System;


namespace ET.Client
{
    public static partial class EnterMapHelper
    {
        public static async ETTask MatchWithAi(Fiber fiber)
        {
            try
            {
                G2C_MatchWithAi g2CEnterMapWithAi = await fiber.Root.GetComponent<ClientSenderCompnent>().Call(new C2G_MatchWithAi()) as G2C_MatchWithAi;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }	
        }
    }
}