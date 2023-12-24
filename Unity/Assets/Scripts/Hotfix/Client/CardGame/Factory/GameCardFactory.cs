namespace ET.Client
{
    public static class GameCardFactory
    {
        public static GameCard Create(Component_Card cards, CardInfo info)
        {
            GameCard card = cards.AddChild<GameCard, CardType>((CardType)info.Type);
            card.SetHP(info.HP);
            card.SetAttack(info.Attack);
            card.SetCost(info.Cost);
            Log.Debug("客户端创建卡牌：" + card);
            return card;
        }
    }
}