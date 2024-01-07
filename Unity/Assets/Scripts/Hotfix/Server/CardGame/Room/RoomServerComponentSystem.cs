using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(RoomServerComponent))]
    [FriendOf(typeof(RoomServerComponent))]
    public static partial class RoomServerComponentSystem
    {
        [EntitySystem]
        private static void Awake(this RoomServerComponent self, List<long> playerIds)
        {
            foreach (long id in playerIds)
            {
                RoomPlayer roomPlayer = self.AddChildWithId<RoomPlayer, long>(id, id);
                if (id == 0) {
                    roomPlayer.AddComponent<RoomAIComponent>();
                }
            }
        }

        public static bool IsAllPlayerProgress100(this RoomServerComponent self)
        {
            foreach (RoomPlayer roomPlayer in self.Children.Values)
            {
                if (roomPlayer.Id == 0) continue;
                if (roomPlayer.Progress != 100)
                {
                    return false;
                }
            }
            return true;
        }
    }
}