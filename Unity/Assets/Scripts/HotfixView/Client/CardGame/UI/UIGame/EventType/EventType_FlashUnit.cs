namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_FlashUnit : AEvent<Scene, FlashUnit>
    {
        protected override async ETTask Run(Scene scene, FlashUnit args) {
            Log.Warning($"处理单位数值刷新消息{args.Card.CardId}");
            await UICGGameHelper.Room2C_FlashUnit(scene.GetComponent<Room>(), args.Card);
        }
    }
}