using System;
using System.Collections.Generic;

namespace ET.Server
{

    [FriendOf(typeof(MatchWithPlayerComponent))]
    public static partial class MatchWithPlayerComponentSystem
    {
        public static async ETTask Match(this MatchWithPlayerComponent self, long playerId)
        {
            Log.Warning("开始匹配");
            if (self.waitMatchPlayers.Contains(playerId))
            {
                return;
            }
            
            self.waitMatchPlayers.Add(playerId);

            if (self.waitMatchPlayers.Count < CGConstValue.MatchCount)
            {
                Log.Warning("人不满就继续等");
                return;
            }
            
            // 申请一个房间
            StartSceneConfig startSceneConfig = RandomGenerator.RandomArray(StartSceneConfigCategory.Instance.Maps);
            Match2Map_GetGameRoom match2MapGetRoom = new();
            foreach (long id in self.waitMatchPlayers)
            {
                match2MapGetRoom.PlayerIds.Add(id);
            }
            
            self.waitMatchPlayers.Clear();

            Scene root = self.Root();
            Map2Match_GetGameRoom map2MatchGetRoom = await root.GetComponent<MessageSender>().Call(
                startSceneConfig.ActorId, match2MapGetRoom) as Map2Match_GetGameRoom;

            Match2C_NotifyGameMatchSuccess match2GNotifyMatchSuccess = new() { ActorId = map2MatchGetRoom.ActorId };
            MessageLocationSenderComponent messageLocationSenderComponent = root.GetComponent<MessageLocationSenderComponent>();
            
            foreach (long id in match2MapGetRoom.PlayerIds) // 这里发送消息线程不会修改PlayerInfo，所以可以直接使用
            {
                messageLocationSenderComponent.Get(LocationType.Player).Send(id, match2GNotifyMatchSuccess);
                // 等待进入房间的确认消息，如果超时要通知所有玩家退出房间，重新匹配
            }
        }
    }
}