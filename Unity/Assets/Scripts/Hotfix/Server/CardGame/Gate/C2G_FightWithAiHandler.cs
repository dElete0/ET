namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    public class C2G_FightWithAiHandler : MessageSessionHandler<C2G_FightWithAi, G2C_FightWithAi>
    {
        protected override async ETTask Run(Session session, C2G_FightWithAi request, G2C_FightWithAi response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            player.RemoveComponent<GateMapComponent>();
            // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");

            Scene scene = gateMapComponent.Scene;
            Log.Debug(scene.SceneType.ToString());

            // 这里可以从DB中加载Player
            GameRoom room = GameRoomFactory.Create(scene, GameRoomType.Ai);
            GamePlayer gamePlayer = GamePlayerFactory.CreatePlayer(scene, room, GamePlayerType.man, player);
            
            //StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Map1");
            response.MyId = player.Id;

            // Todo 服务器发送进入战斗场景的逻辑
            Log.Warning("Todo 服务器发送进入战斗场景的逻辑");

            // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
            //TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, startSceneConfig.Name).Coroutine();
        }
    }
}