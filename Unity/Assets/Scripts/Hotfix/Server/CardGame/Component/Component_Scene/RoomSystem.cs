using System.Collections.Generic;

namespace ET.Server {
    [EntitySystemOf(typeof(GameRoom))]
    [FriendOfAttribute(typeof(ET.GameRoom))]
    [FriendOfAttribute(typeof(ET.Component_Room_GamePlayer))]
    [FriendOfAttribute(typeof(ET.Component_Player_Group))]
    [FriendOfAttribute(typeof(ET.Component_Player_Hero))]
    [FriendOfAttribute(typeof(ET.Component_Player_Agents))]
    public static partial class RoomSystem
    {
        [EntitySystem]
        private static void Awake(this ET.GameRoom self, ET.GameRoomType args2)
        {
            Log.Debug("Server: 创建了一个房间");
            self.state = GameState.Wait;
        }
        [EntitySystem]
        private static void Update(this ET.GameRoom self)
        {
            switch (self.state)
            {
                case GameState.Wait:
                    {
                        Component_Room_GamePlayer players = self.GetComponent<Component_Room_GamePlayer>();
                        if (players.PlayerMax == players.PlayerCount) {
                            self.state = GameState.Build;
                            Log.Debug("房间进入Build状态");
                            EventSystem.Instance.Publish(self.Parent.Scene(), new GameEventType_GameStart() {room = self});
                        } else if (players.PlayerMax < players.PlayerCount) {
                            Log.Error("房间人数还能超了么？");
                        }
                        break;
                    }
                case GameState.Run:
                    {
                        break;
                    }
            }
        }
    }
}