namespace ET.Client
{
    [MessageHandler(SceneType.CardGame)]
    public class Room2C_LoseArmorHandler: MessageHandler<Scene, Room2C_LoseArmor> {
        protected override async ETTask Run(Scene root, Room2C_LoseArmor message) {
            
            await ETTask.CompletedTask;
        }
    }
}