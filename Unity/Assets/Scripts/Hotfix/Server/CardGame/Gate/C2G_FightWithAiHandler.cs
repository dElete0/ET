namespace ET.Server
{
    [MessageSessionHandler(SceneType.Gate)]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    public class C2G_FightWithAiHandler : MessageSessionHandler<C2G_FightWithAi, G2C_FightWithAi>
    {
        protected override async ETTask Run(Session session, C2G_FightWithAi request, G2C_FightWithAi response)
        {
            Player player = session.GetComponent<SessionPlayerComponent>().Player;
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");
            Scene scene = gateMapComponent.Scene;
            
            // 先在Gate上创建一个GameRoom,随后创建两个GamePlayer,随后将两个unit传送到Map上，最后让room失效
            
            GameRoom room = GameRoomFactory.Create(session.Root(), GameRoomType.Ai);
            GamePlayer aiPlayer = GamePlayerFactory.CreatePlayer(room, GamePlayerType.ai);
            GamePlayer gamePlayer = GamePlayerFactory.CreatePlayer(room, GamePlayerType.man);
            aiPlayer.unit = UnitFactory.Create(scene, 0, UnitType.Monster);
            gamePlayer.unit = UnitFactory.Create(scene, player.Id, UnitType.Player);
            
            //StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Map1");
            response.MyId = player.Id;

            // Todo 服务器发送进入战斗场景的逻辑
            Log.Warning("Todo 服务器发送进入战斗场景的逻辑");

            await ETTask.CompletedTask;
        }
    }
}