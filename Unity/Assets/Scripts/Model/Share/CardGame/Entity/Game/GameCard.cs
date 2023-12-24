namespace ET {
    [ChildOf(typeof(Component_Card))]
    //游戏内的卡牌
    public class GameCard : Entity, IAwake<CardType>, IDestroy {
        public CardType CardType;
        
        //info
        public int CardBaseId;

        //基础属性
        public int HPD;//默认血量
        public int HPMax;//最大血量
        public int HP;//当前血量
        public int Attack;
        public int AttackD;
        
        //资质需求
        public int Red;
        public int RedD;
        public int Green;
        public int GreenD;
        public int Blue;
        public int BlueD;
        public int Black;
        public int BlackD;
        public int White;
        public int WhiteD;
        public int Gruy;
        public int GruyD;
        
        //费用需求
        public int Cost;
        public int CostD;
    }

    public enum CardType {
        Magic = 0,
        Unit = 1,
        Plot = 2,
        Agent = 3,
        Hero = 4,
    }

    public enum CardColor {
        none = 0,
        red = 1,
        green = 2,
        blue = 3,
        black = 4,
        white = 5,
        gruy = 6,
    }

    public enum CardPos {
        None = 0,//墓地，未加入手牌等
        Hand = 1,//手牌
        Group = 2,
    }
}
