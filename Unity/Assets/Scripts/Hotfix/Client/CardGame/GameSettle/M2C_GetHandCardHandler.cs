namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_GetHandCardHandler : MessageHandler<Unit, M2C_GetHandCard>
    {
        protected override ETTask Run(Unit entity, M2C_GetHandCard message) { throw new System.NotImplementedException(); }
    }
}