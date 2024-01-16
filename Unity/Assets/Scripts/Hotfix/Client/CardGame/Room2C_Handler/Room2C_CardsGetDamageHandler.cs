namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_CardsGetDamageHandler: MessageHandler<Scene, Room2C_CardsGetDamage>
    {
        protected override async ETTask Run(Scene root, Room2C_CardsGetDamage message) {
            await EventSystem.Instance.PublishAsync(root, new CardsGetDamage() {Card = message.Card, Hurt = message.hurt});
        }
    }
}