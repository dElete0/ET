namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_NewHeroHandler : MessageHandler<Scene, Room2C_NewHero>
    {
        protected override async ETTask Run(Scene root, Room2C_NewHero message) {
            await EventSystem.Instance.PublishAsync(root, new NewHero() {Hero = message.Hero});
        }
    }
}