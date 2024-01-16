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

        public static async ETTask CardGetDamage(Room room, RoomCardInfo cardInfo, int hurt) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            
            UIUnitInfo uiUnitInfo = uicgGameComponent.GetUIUnitInfoById(cardInfo.CardId);
            
            if (uiUnitInfo == null) Log.Error($"获取UIUnitInfo失败:{cardInfo.CardId}");
            uicgGameComponent.GetComponent<UIAnimComponent>()
                    .AppendCallback(() => {
                        uiUnitInfo.HP.text = cardInfo.HP.ToString();
                        uiUnitInfo.ShowCardGetDamage(room, hurt).Coroutine();
                    });
        }
    }
}