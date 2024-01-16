using System;
using System.Collections.Generic;

namespace ET.Client
{
    [Event(SceneType.CardGame)]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    public class EventType_CardsGetDamage : AEvent<Scene, CardsGetDamage>
    {
        protected override async ETTask Run(Scene scene, CardsGetDamage args)
        {
            await CardsGetDamage(scene.GetComponent<Room>(), args.Card, args.Hurt);
        }

        public static async ETTask CardsGetDamage(Room room, List<RoomCardInfo> cardInfo, List<int> hurt) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();

            List<Action> actions = new List<Action>();
            for (int i = 0; i < cardInfo.Count; i++) {
                UIUnitInfo uiUnitInfo = uicgGameComponent.GetUIUnitInfoById(cardInfo[i].CardId);
                if (uiUnitInfo == null) Log.Error($"获取UIUnitInfo失败:{cardInfo[i].CardId}");
                actions.Add(() => {
                    uiUnitInfo.HP.text = cardInfo[i].HP.ToString();
                    uiUnitInfo.ShowCardGetDamage(room, hurt[i]).Coroutine();
                });
            }
            uicgGameComponent.GetComponent<UIAnimComponent>()
                    .AppendCallback(() => {
                        actions.ForEach(action => action.Invoke());
                    });
        }
    }
}