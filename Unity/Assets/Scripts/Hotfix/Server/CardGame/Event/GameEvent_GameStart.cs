using System.Collections.Generic;

namespace ET.Server {
    [Event(SceneType.Map)]
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
            Log.Debug("执行游戏开始逻辑");
            GamePlayer player1 = eventType.room.GetComponent<Component_Room_GamePlayer>().GetChild<GamePlayer>(1);
            GamePlayer player2 = eventType.room.GetComponent<Component_Room_GamePlayer>().GetChild<GamePlayer>(2);
            //两位玩家进入Map服务器
            Log.Warning("两位玩家进入Map服务器");
            await PlayerEnterToMap(eventType.room, scene, player1);
            await PlayerEnterToMap(eventType.room, scene, player2);
            //对局先后手
            int a = RandomGenerator.RandomNumber(0, 2);
            //初始化双方卡组等
            SetGroup(player1);
            SetGroup(player2);
            //先后手
            if (a == 0)
            {
                eventType.room.NowPlayer = player1;
            }
            else
            {
                eventType.room.NowPlayer = player2;
            }
            Log.Warning("Player1组件：" + player1.GetComponent<Component_Card>());
            Log.Warning("Player2组件：" + player2.GetComponent<Component_Card>());
            Log.Warning(eventType.room.ToString());
            //抽卡
            if (eventType.room.NowPlayer == player1)
            {
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_GetHandCardsFromGroup() { Room = eventType.room, Cards = player1.GetComponent<Component_Card>(), Count = 3 });
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_GetHandCardsFromGroup() { Room = eventType.room, Cards = player2.GetComponent<Component_Card>(), Count = 4 });
            }
            else
            {
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_GetHandCardsFromGroup() { Room = eventType.room, Cards = player1.GetComponent<Component_Card>(), Count = 4 });
                await EventSystem.Instance.PublishAsync(scene, new GameEventType_GetHandCardsFromGroup() { Room = eventType.room, Cards = player2.GetComponent<Component_Card>(), Count = 3 });
            }

            eventType.room.state = GameState.Run;
            Log.Debug("房间进入Run状态");
            await EventSystem.Instance.PublishAsync(scene, new GameEventType_GameStartOver() { room = eventType.room });
            await ETTask.CompletedTask;
        }

        //玩家进入Map服务器
        protected async ETTask PlayerEnterToMap(GameRoom room, Scene scene, GamePlayer gamePlayer)
        {
            Log.Warning("进入Map服务器");
            // 在Gate上动态创建一个Map Scene，把Unit从DB中加载放进来，然后传送到真正的Map中，这样登陆跟传送的逻辑就完全一样了
            Player player = gamePlayer.GetComponent<ServerComponent_Player>().player;
            GateMapComponent gateMapComponent = player.GetComponent<GateMapComponent>();
            if (gateMapComponent == null) gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, gamePlayer.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");
            

            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(gamePlayer.Zone(), "Map1");

            // 等到一帧的最后面再传送，先让G2C_EnterMap返回，否则传送消息可能比G2C_EnterMap还早
            await GameRoomStartHelper.GameRoomStart(room.Id, gamePlayer.Id , gamePlayer.unit, startSceneConfig.ActorId, startSceneConfig.Name);
            //await room.GetComponent<ObjectWait>().Wait<Wait_GameEventType_UnitRebuildFinish>();
        }

        //玩家初始化
        private static void SetGroup(GamePlayer player)
        {
            Component_Card cards = player.AddComponent<Component_Card>();
            var group = cards.AddComponent<Component_Player_Group>();
            var hero = cards.AddComponent<Component_Player_Hero>();
            var agents = cards.AddComponent<Component_Player_Agents>();
            var gameUnits = cards.AddComponent<Component_Player_Units>();
            var handCards = cards.AddComponent<Component_Player_HandCards>();
            
            //场上单位
            gameUnits.units = new List<GameCard>();
            
            //手牌
            handCards.HandCards = new List<GameCard>();

            //牌库
            group.cards = new List<GameCard>();
            for (int i = 0; i < 30; i++)
            {
                group.cards.Add(GameCardFactory.CreateUnitCard(cards));
            }

            //英雄
            hero.Hero = GameCardFactory.CreateHero(cards);

            //干员
            agents.Agent1 = GameCardFactory.CreateAgent(cards);
            agents.Agent2 = GameCardFactory.CreateAgent(cards);
        }
    }
}