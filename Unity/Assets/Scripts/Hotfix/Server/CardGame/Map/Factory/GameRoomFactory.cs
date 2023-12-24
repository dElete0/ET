using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    public static partial class GameRoomFactory
    {
        public static GameRoom Create(Scene scene, GameRoomType type)
        {
            Component_Rooms rooms = scene.GetComponent<Component_Rooms>();
            switch (type)
            {
                case GameRoomType.Ai:
                    {
                        GameRoom room = rooms.AddChild<GameRoom, GameRoomType>(GameRoomType.Ai);
                        room.AddComponent<Component_Room_GamePlayer, int>(2);
                        GamePlayer aiPlayer = GamePlayerFactory.CreatePlayer(scene, room, GamePlayerType.ai, null);
                        return room;
                    }
                default:
                    {
                        throw new Exception($"not such unit type: {type}");
                    }
            }
        }
    }
}