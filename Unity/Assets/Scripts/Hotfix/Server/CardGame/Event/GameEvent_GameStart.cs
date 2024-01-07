using System.Collections.Generic;

namespace ET.Server {
    [Event(SceneType.Gate)]
    [FriendOfAttribute(typeof(ET.Component_Player_Group))]
    [FriendOfAttribute(typeof(ET.Component_Player_Hero))]
    [FriendOfAttribute(typeof(ET.Component_Player_Agents))]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    [FriendOfAttribute(typeof(ET.Server.ServerComponent_Player))]
    [FriendOfAttribute(typeof(ET.Component_Player_HandCards))]
    [FriendOfAttribute(typeof(ET.Component_Player_Units))]
    public class GameEvent_GameStart : AEvent<Scene, GameEventType_GameStart>
    {
        protected override async ETTask Run(Scene scene, GameEventType_GameStart eventType)
        {
            
            await RoomEnterToMap(eventType.room, scene);
            await ETTask.CompletedTask;
        }
        
        //Room进入Map服务器
        private async ETTask RoomEnterToMap(GameRoom room, Scene scene) {
            
            Log.Warning("Room进入Map服务器");

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(room.Zone(), "Map1");

            // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
            await GameRoomStartHelper.GameRoomMoveToMap(room, startSceneConfig.ActorId, startSceneConfig.Name);
        }

        //玩家进入Map服务器
        protected async ETTask PlayerEnterToMap(GameRoom room, Scene scene, GamePlayer gamePlayer)
        {
            Log.Warning("进入Map服务器");
            Player player = gamePlayer.GetComponent<ServerComponent_Player>().player;
            
            // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, gamePlayer.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");
            
            // 这里可以从DB中加载Unit
            // 先在gateMapComponent。Scene上创建Unit,随后在Root上正式创建
            SetPlayerUnit(scene, gamePlayer);
            
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(gamePlayer.Zone(), "Map1");

            // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
            await GameRoomStartHelper.GameRoomStart(room.Id, gamePlayer.Id , gamePlayer.unit, startSceneConfig.ActorId, startSceneConfig.Name);
            //await room.GetComponent<ObjectWait>().Wait<Wait_GameEventType_UnitRebuildFinish>();
        }
        
        //创建Unit
        private static void SetPlayerUnit(Scene scene, GamePlayer player) {
            Unit unit = UnitFactory.Create(scene, player.Id, player.IsBot ? UnitType.Monster : UnitType.Player);
            Log.Debug("建立Actor机制,并连接：Unit:" + unit.Id);
            player.unit = unit;
        }
    }
}