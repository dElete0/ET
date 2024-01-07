namespace ET {
    
    [ChildOf(typeof(Component_Rooms))]
    public partial class GameRoom : Entity, IAwake<GameRoomType>, IUpdate {
        public GameRoomType type;
        public GameState state;
        private EntityRef<GamePlayer> nowPlayer;

        public GamePlayer NowPlayer {
            get
            {
                return this.nowPlayer;
            }
            set
            {
                this.nowPlayer = value;
            }
        }
    }

    public enum GameRoomType {
        None = 0,
        Ai = 1, //人机
        Match = 2, //匹配
    }

    public enum GameState {
        None = 0,
        Run = 1, //正在进行
        Wait = 2, //等待人齐
        Over = 3, //对局结束
        Build = 4, //创建房间中
    }
}
