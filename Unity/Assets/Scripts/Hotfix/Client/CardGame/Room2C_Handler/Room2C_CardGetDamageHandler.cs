namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CardGetDamageHandler: MessageHandler<Scene, Room2C_CardGetDamage>
    {
        protected override async ETTask Run(Scene root, Room2C_CardGetDamage message) {
            await EventSystem.Instance.PublishAsync(root, new CardGetDamage() {Card = message.Card, Hurt = message.hurt});
        }
    }
}