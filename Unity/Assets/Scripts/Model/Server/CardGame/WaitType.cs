namespace ET.Server
{
    namespace WaitType
    {
        public struct Wait_C2Room_Select : IWaitType
        {
            public int Error { get; set; }

            public C2Room_SelectCard Message;
        }
    }
}