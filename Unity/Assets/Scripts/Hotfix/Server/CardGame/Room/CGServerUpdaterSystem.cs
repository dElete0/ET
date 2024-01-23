using System;
using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(CGServerUpdater))]
    [FriendOf(typeof(CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    public static partial class CGServerUpdaterSystem
    {
        [EntitySystem]
        private static void Awake(this CGServerUpdater self, GameRoomType type)
        {
            self.GameState = GameState.Build;
            self.TurnTimeMax = CGServerUpdater.TurmTimeD;
            self.GameRoomType = type;
            self.TurnStartTime = TimeInfo.Instance.ServerFrameTime();

            // 初始化房间数据结构
            self.GetParent<Room>().AddComponent<CardGameComponent_Cards>();
            RoomEventTypeComponent roomEventTypeComponent = self.GetParent<Room>().AddComponent<RoomEventTypeComponent>();

            RoomServerComponent roomServerComponent = self.GetParent<Room>().GetComponent<RoomServerComponent>();
            foreach (RoomPlayer roomPlayer in roomServerComponent.Children.Values) {
                roomPlayer.AddComponent<CardGameComponent_Player>();
                roomPlayer.AddComponent<PlayerEventTypeComponent, RoomEventTypeComponent>(roomEventTypeComponent);
            }

            //初始化游戏
            CGRoomHelper.RoomCardInit(self.GetParent<Room>()).Coroutine();
        }

        [EntitySystem]
        private static void Update(this CGServerUpdater self)
        {
            if (self.GameRoomType == GameRoomType.Match) {
                long timeNow = TimeInfo.Instance.ServerFrameTime();

                if (timeNow < self.TurnTimeMax + self.TurnStartTime)
                {
                    self.TurnStartTime = timeNow;

                    // todo 强制回合结束
                    return;
                }
            }
        }
    }
}