using System;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    [FriendOfAttribute(typeof(ET.Server.ServerComponent_Player))]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    public static partial class GamePlayerFactory
    {
        public static GamePlayer CreatePlayer(Scene scene, GameRoom room, GamePlayerType type, Player sessionPlayer)
        {
            Component_Room_GamePlayer players = room.GetComponent<Component_Room_GamePlayer>();
            if (players.PlayerMax <= players.PlayerCount)
            {
                Log.Debug("房间人数已经满了");
                return null;
            }

            players.PlayerCount++;

            switch (type)
            {
                case GamePlayerType.man:
                    {
                        GamePlayer player = players.AddChildWithId<GamePlayer, GamePlayerType>(players.PlayerCount, GamePlayerType.man);
                        Log.Warning("增加了1个玩家到房间：" + room.Id);
                        player.AddComponent<ServerComponent_Player>().player = sessionPlayer;
                        SetPlayerUnit(scene, player);
                        return player;
                    }
                case GamePlayerType.ai:
                    {
                        GamePlayer player = players.AddChildWithId<GamePlayer, GamePlayerType>(players.PlayerCount, GamePlayerType.man);
                        Log.Warning("增加了1个AI到房间：" + room.Id);
                        Player childPlayer = room.Root().GetComponent<PlayerComponent>().AddChild<Player, string>("AI" + player.InstanceId);
                        player.AddComponent<ServerComponent_Player>().player = childPlayer;
                        player.IsBot = true;
                        SetPlayerUnit(scene, player);
                        player.AddComponent<GameAI>();
                        return player;
                    }
                default:
                    throw new Exception($"not such unit type: {type}");
            }
        }

        private static void SetPlayerUnit(Scene scene, GamePlayer player) {
            Unit unit = UnitFactory.Create(scene, player.Id, player.IsBot ? UnitType.Monster : UnitType.Player);
            Log.Debug("建立Actor机制,并连接：Unit:" + unit.Id);
            player.unit = unit;
        }
    }
}