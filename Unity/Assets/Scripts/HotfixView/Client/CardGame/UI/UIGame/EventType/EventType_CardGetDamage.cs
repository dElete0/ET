namespace ET.Client
{
    [Event(SceneType.CardGame)]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    public class EventType_CardGetDamage : AEvent<Scene, CardGetDamage>
    {
        protected override async ETTask Run(Scene scene, CardGetDamage args)
        {
            await CardGetDamage(scene.GetComponent<Room>(), args.Card, args.Hurt);
        }

        private static UIUnitInfo GetUIUnitInfoById(UICGGameComponent uicgGameComponent, long id) {
            foreach (var cardUnitInfo in uicgGameComponent.HeroAndAgent)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.MyFightUnits)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.EnemyFightUnits)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }

            return null;
        }

        public static async ETTask CardGetDamage(Room room, RoomCardInfo cardInfo, int hurt) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            
            foreach (var fightUnit in uicgGameComponent.MyFightUnits) {
                Log.Warning($"目前有的单位:{fightUnit.CardId}");
            }
            UIUnitInfo uiUnitInfo = GetUIUnitInfoById(uicgGameComponent, cardInfo.CardId);
            
            if (uiUnitInfo == null) Log.Error($"获取UIUnitInfo失败:{cardInfo.CardId}");
            uicgGameComponent.GetComponent<UIAnimComponent>()
                    .AppendCallback(() => {
                        uiUnitInfo.HP.text = cardInfo.HP.ToString();
                        uiUnitInfo.ShowCardGetDamage(room, hurt).Coroutine();
                    });
        }
    }
}