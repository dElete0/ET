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

        public static async ETTask CardGetDamage(Room room, RoomCardInfo cardInfo, int hurt)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            UIUnitInfo info = null;
            foreach (var cardUnitInfo in uicgGameComponent.HeroAndAgent)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.MyFightUnits)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.EnemyFightUnits)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(
                () => info.HP.text = cardInfo.HP.ToString());
        }
    }
}