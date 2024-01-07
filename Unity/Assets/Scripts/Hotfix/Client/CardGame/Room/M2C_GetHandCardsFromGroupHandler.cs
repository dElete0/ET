using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;

namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    [FriendOfAttribute(typeof(ET.Component_Player_HandCards))]
    [FriendOfAttribute(typeof(ET.Client.Component_Unit_RoomInfo))]
    public class M2C_GetHandCardsFromGroupHandler : MessageHandler<Scene, M2C_GetHandCardsFromGroup>
    {
        protected override async ETTask Run(Scene root, M2C_GetHandCardsFromGroup message)
        {
            Log.Debug("接受Sever Message:" + message.GetType());
            Unit unit = root.CurrentScene().GetComponent<UnitComponent>().Get(message.Id);
            if (unit == null)
            {
                return;
            }

            /*Component_Card cards = unit.GetComponent<Component_Unit_RoomInfo>().Player.GetComponent<Component_Card>();
            Component_Player_HandCards handCards = cards.GetComponent<Component_Player_HandCards>();
            GameCard card = GameCardFactory.Create(cards, message.Card);
            handCards.HandCards.Add(card);*/
            await ETTask.CompletedTask;
        }
    }
}