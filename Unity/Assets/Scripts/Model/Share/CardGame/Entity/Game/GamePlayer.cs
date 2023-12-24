namespace ET {
    
    [ChildOf(typeof(Component_Room_GamePlayer))]
    public partial class GamePlayer : Entity, IAwake<GamePlayerType>, IUpdate {
        //基本信息
        public bool IsBot;
        public GamePlayerType GamePlayerType;
        public Unit unit { get; set; }

        //费用
        public int CostMax;
        public int Cost;
        public int CostD;
    }

    public enum GamePlayerType {
        none = 0,
        man = 1,
        ai = 2,
    }
}