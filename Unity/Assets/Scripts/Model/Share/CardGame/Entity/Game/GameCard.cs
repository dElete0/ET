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
}
