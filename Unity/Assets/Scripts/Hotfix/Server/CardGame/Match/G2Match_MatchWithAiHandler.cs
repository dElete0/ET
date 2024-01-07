using System;


namespace ET.Server
{
    [MessageHandler(SceneType.Match)]
    public class G2Match_MatchWithAiHandler : MessageHandler<Scene, G2Match_MatchWithAi, Match2G_MatchWithAi>
    {
        protected override async ETTask Run(Scene scene, G2Match_MatchWithAi request, Match2G_MatchWithAi response)
        {
            MatchWithAiComponent matchComponent = scene.GetComponent<MatchWithAiComponent>();
            matchComponent.Match(request.Id).Coroutine();
            await ETTask.CompletedTask;
        }
    }
}