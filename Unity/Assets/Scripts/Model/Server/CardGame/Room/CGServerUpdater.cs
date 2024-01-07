using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Room))]
    public class CGServerUpdater: Entity, IAwake, IUpdate {
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
    }
}