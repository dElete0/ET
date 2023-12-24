namespace ET.Client {
    [EntitySystemOf(typeof(GameCard))]
    [FriendOfAttribute(typeof(ET.GameCard))]
    public static partial class GameCardSystem
    {
        [EntitySystem]
        private static void Awake(this ET.GameCard self, CardType type)
        {
            self.CardType = type;
        }

        [EntitySystem]
        private static void Destroy(this ET.GameCard self)
        {

        }

        public static void SetCost(this ET.GameCard self, int cost) {
            self.Cost = cost;
            self.CostD = cost;
        }

        public static void SetAttack(this ET.GameCard self, int attack) {
            self.Attack = attack;
            self.AttackD = attack;
        }

        public static void SetHP(this ET.GameCard self, int hp) {
            self.HP = hp;
            self.HPD = hp;
            self.HPMax = hp;
        }
    }
}