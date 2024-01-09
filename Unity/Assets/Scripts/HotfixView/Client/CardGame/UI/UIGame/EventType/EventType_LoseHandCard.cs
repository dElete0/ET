namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EventType_LoseHandCard : AEvent<Scene, LoseHandCard>
    {
        protected override async ETTask Run(Scene scene, LoseHandCard args) {
            await UICGGameHelper.Room2C_LoseHandCard(scene.GetComponent<Room>(), args.CardId);
        }
    }
}