namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_EnemyNewHeroHandler : MessageHandler<Scene, Room2C_EnemyNewHero>
    {
        protected override async ETTask Run(Scene root, Room2C_EnemyNewHero message) {
            await EventSystem.Instance.PublishAsync(root, new EnemyNewHeroType() {Hero = message.Hero});
        }
    }
}