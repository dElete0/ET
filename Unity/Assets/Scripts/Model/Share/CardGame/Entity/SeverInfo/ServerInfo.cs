namespace ET {

    public class ServerInfo : Entity, IAwake {
        
    }

    //服务器状态
    public enum ServerInfoType {
        none = 0,
        run = 1, //运行
        close = 2, //关闭
        fix = 3, //维护
    }
}