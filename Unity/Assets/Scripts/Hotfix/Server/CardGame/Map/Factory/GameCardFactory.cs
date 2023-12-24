using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.GameCard))]
    public static partial class GameCardFactory
    {
        public static GameCard CreateUnitCard(Component_Card cards)
        {
            GameCard card = cards.AddChild<GameCard, CardType>(CardType.Unit);
            card.CardBaseId = 1000;
            card.SetCost(3);
            card.SetAttack(3);
            card.SetHP(6);
            return card;
        }

        public static GameCard CreateHero(Component_Card hero) {
            Log.Debug("创建英雄");
            GameCard card = hero.AddChild<GameCard, CardType>(CardType.Hero);
            card.CardBaseId = 10;
            card.SetHP(30);
            return card;
        }

        public static GameCard CreateAgent(Component_Card agents) {
            GameCard card = agents.AddChild<GameCard, CardType>(CardType.Agent);
            card.CardBaseId = 100;
            card.SetAttack(5);
            card.SetCost(5);
            card.SetHP(5);
            return card;
        }
    }
}