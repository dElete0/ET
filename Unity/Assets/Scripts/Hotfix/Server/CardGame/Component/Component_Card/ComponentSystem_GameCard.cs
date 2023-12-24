namespace ET.Server {
    [EntitySystemOf(typeof(GameCard))]
    [FriendOfAttribute(typeof(ET.GameCard))]
    public static partial class ComponentSystem_GameCard
    {
        [EntitySystem]
        private static void Awake(this ET.GameCard self, ET.CardType args2)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.GameCard self)
        {

        }
        public static void Send_GetHandCard(this Unit unit, GameCard card) {
            MapMessageHelper.SendToClient(unit, new M2C_GetHandCard()
            {
                Error = 0,
                Card = new() {
                    Type = (int)card.CardType,
                    Attack = card.Attack,
                    HP = card.HP,
                    Cost = card.Cost,
                }
            });
        }
    }
}