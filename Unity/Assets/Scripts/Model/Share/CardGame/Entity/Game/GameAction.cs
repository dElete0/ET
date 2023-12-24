namespace ET {
    
    //游戏中的玩家动作
    [ChildOf(typeof(GameRoom))]
    public partial class GameAction : Entity, IAwake, IUpdate {
        public GameActionType type;
    }

    //玩家在游戏中的行为
    public enum GameActionType {
        None = 0,
        Attack = 1,
        TurnOver = 2,
        UseAgent1 = 3,
        UseAgent2 = 4,
        GiveUp = 5,
        UseHandCard = 6,
        Select = 7,
    }
}