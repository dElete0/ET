namespace ET.Server {
    [EntitySystemOf(typeof(Component_Card_Hero))]
    [FriendOfAttribute(typeof(ET.Component_Card_Hero))]
    public static partial class ComponetnSystem_Card_Hero
    {
        [EntitySystem]
        private static void Awake(this ET.Component_Card_Hero self)
        {
            self.color1 = CardColor.Red;
            self.color2 = CardColor.Red;
        }
    }
}