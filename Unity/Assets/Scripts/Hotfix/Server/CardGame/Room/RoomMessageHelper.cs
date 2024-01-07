namespace ET.Server
{

    public static partial class RoomMessageHelper
    {
        public static void BroadCast(Room room, IMessage message)
        {
            // 广播的消息不能被池回收
            (message as MessageObject).IsFromPool = false;
            
            RoomServerComponent roomServerComponent = room.GetComponent<RoomServerComponent>();

            MessageLocationSenderComponent messageLocationSenderComponent = room.Root().GetComponent<MessageLocationSenderComponent>();
            foreach (var kv in roomServerComponent.Children)
            {
                RoomPlayer roomPlayer = kv.Value as RoomPlayer;

                if (!roomPlayer.IsOnline)
                {
                    continue;
                }
                
                messageLocationSenderComponent.Get(LocationType.GateSession).Send(roomPlayer.Id, message);
            }
        }

        // 除了目标以外的角色接受消息
        public static void BroadCastWithOutPlayer(RoomPlayer player, IMessage message) {
            // 广播的消息不能被池回收
            (message as MessageObject).IsFromPool = false;

            RoomServerComponent roomServerComponent = player.GetParent<Room>().GetComponent<RoomServerComponent>();

            MessageLocationSenderComponent messageLocationSenderComponent = player.GetParent<Room>().Root().GetComponent<MessageLocationSenderComponent>();
            foreach (var kv in roomServerComponent.Children) {
                RoomPlayer roomPlayer = kv.Value as RoomPlayer;

                if (player == roomPlayer || !roomPlayer.IsOnline) {
                    continue;
                }

                messageLocationSenderComponent.Get(LocationType.GateSession).Send(roomPlayer.Id, message);
            }
        }

        public static RoomPlayer GetNextRoomPlayer(this RoomPlayer player) {
            RoomServerComponent roomServerComponent = player.GetParent<Room>().GetComponent<RoomServerComponent>();

            bool isNext = false;
            foreach (var kv in roomServerComponent.Children) {
                RoomPlayer roomPlayer = kv.Value as RoomPlayer;
                if (isNext) {
                    return roomPlayer;
                }
                if (player == roomPlayer) {
                    isNext = true;
                }
            }

            foreach (var kv in roomServerComponent.Children) {
                return kv.Value as RoomPlayer;
            }

            return null;
        }
        
        // 通知目标角色
        public static void ServerSendMessageToClient(RoomPlayer player, IMessage message) {
            (message as MessageObject).IsFromPool = false;
            MessageLocationSenderComponent messageLocationSenderComponent = player.GetParent<Room>().Root().GetComponent<MessageLocationSenderComponent>();
            messageLocationSenderComponent.Get(LocationType.GateSession).Send(player.Id, message);
        }
    }
}