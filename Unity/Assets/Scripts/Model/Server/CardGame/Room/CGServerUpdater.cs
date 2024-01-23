using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Room))]
    public class CGServerUpdater: Entity, IAwake<GameRoomType>, IUpdate {
        //房间状态
        public GameState GameState;
        //当前玩家(RoomPlayerId)
        public long NowPlayer;
        //本回合需要等待的时间
        public long TurnTimeMax;
        //本回合开始的时间
        public long TurnStartTime;
        //默认一回合等待的时间
        public const long TurmTimeD = 60000;
        //房间类型
        public GameRoomType GameRoomType;
        /// <summary>
        /// 等待玩家选择发现或抉择效果
        /// -1:没有进行发现，正常打牌
        /// 0:取消这次发现
        /// -2:等待发现卡牌，才能进行其他操作
        /// </summary>
        public long FindCardId = -1;
    }
}