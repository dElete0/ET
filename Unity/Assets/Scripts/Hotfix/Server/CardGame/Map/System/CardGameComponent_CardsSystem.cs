namespace ET.Server {
    [EntitySystemOf(typeof(CardGameComponent_Cards))]
    [FriendOfAttribute(typeof(ET.CardGameComponent_Cards))]
    public static partial class CardGameComponent_CardsSystem
    {
        [EntitySystem]
        private static void Awake(this ET.CardGameComponent_Cards self)
        {
        }
    }
}