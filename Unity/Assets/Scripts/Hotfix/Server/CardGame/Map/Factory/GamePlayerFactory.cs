using System;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    [FriendOfAttribute(typeof(ET.Server.ServerComponent_Player))]
    [FriendOfAttribute(typeof(ET.GamePlayer))]
    public static partial class GamePlayerFactory
    {
        public static GamePlayer CreatePlayer(GameRoom room, GamePlayerType type)
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
                        return player;
                    }
                case GamePlayerType.ai:
                    {
                        GamePlayer player = players.AddChildWithId<GamePlayer, GamePlayerType>(players.PlayerCount, GamePlayerType.man);
                        Log.Warning("增加了1个AI到房间：" + room.Id);
                        player.IsBot = true;
                        player.AddComponent<GameAI>();
                        return player;
                    }
                default:
                    throw new Exception($"not such unit type: {type}");
            }
        }
    }
}