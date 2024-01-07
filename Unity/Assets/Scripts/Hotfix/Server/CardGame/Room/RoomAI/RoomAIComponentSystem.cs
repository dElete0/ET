using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(RoomAIComponent))]
    [FriendOf(typeof(RoomAIComponent))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static partial class RoomAIComponentSystem
    {
        [EntitySystem]
        private static void Awake(this RoomAIComponent self)
        {
        }

        [EntitySystem]
        private static void Update(this RoomAIComponent self) {
            if (self.GetParent<Room>().GetComponent<CGServerUpdater>() == null) return;
            if (self.IsToDo) return;
            if (self.GetParent<Room>().GetComponent<CGServerUpdater>().NowPlayer == self.GetParent<RoomPlayer>().Id) {
                // AI自己结束自己回合
                Log.Warning("AI自己结束自己回合");
                self.IsToDo = true;
                C2Room_TurnOverHandler.AI2Room_TurnOver(self, new C2Room_TurnOver() { }).Coroutine();
            }
        }
    }
}