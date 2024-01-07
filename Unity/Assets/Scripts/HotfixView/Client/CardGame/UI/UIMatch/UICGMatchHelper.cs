namespace ET.Client
{
    public static partial class UICGMatchHelper {
        public static async ETTask Login(Scene root, string account, string password)
        {
            root.RemoveComponent<ClientSenderCompnent>();
            ClientSenderCompnent clientSenderCompnent = root.AddComponent<ClientSenderCompnent>();

            long playerId = root.GetComponent<PlayerComponent>().MyId;
            
            
            //玩家从Gate上得到的Key
            NetClient2Main_Login response = await clientSenderCompnent.LoginAsync(account, password);

            if (response.Error != ErrorCode.ERR_Success) {
                Log.Error($"response Error :{ response.Error}");
                return;
            }
            
            // await EventSystem.Instance.PublishAsync(root, new LoginFinish());
        }
    }
}