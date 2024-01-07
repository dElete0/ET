using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    public static partial class GameRoomFactory
    {
        public static GameRoom Create(Scene scene, GameRoomType type)
        {
            scene.RemoveComponent<Component_Rooms>();
            Component_Rooms rooms = scene.AddComponent<Component_Rooms>();
            switch (type)
            {
                case GameRoomType.Ai:
                    {
                        GameRoom room = rooms.AddChild<GameRoom, GameRoomType>(GameRoomType.Ai);
                        room.AddComponent<Component_Room_GamePlayer, int>(2);
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