namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_MatchWithAiHandler : MessageSessionHandler<C2G_MatchWithAi, G2C_MatchWithAi>
    {
        protected override async ETTask Run(Session session, C2G_MatchWithAi request, G2C_MatchWithAi response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.Match;

            await session.Root().GetComponent<MessageSender>().Call(startSceneConfig.ActorId,
                new G2Match_MatchWithAi() { Id = player.Id });
        }
    }
}