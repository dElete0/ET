using System;
using System.Collections.Generic;

namespace ET {

    public struct GameEventType_GameStart {
        public GameRoom room;
    }
    
    public struct GameEventType_Attack {
        public GameCard Actor;
        public GameCard Target;
    }

    public struct GameEventType_GetHandCardsFromGroup {
        public RoomPlayer Target;
        public Room Room;
        public int Count;
    }

    public struct GameEventType_GetHandCard {
        public RoomPlayer Player;
        public long Card;
        public List<long> HandCards;
    }

    public struct GameEventType_RemoveCardFromGroup {
        public long Card;
        public List<long> Group;
    }

    public struct GameEventType_TurnOver {
        public GamePlayer Player;
    }

    public struct GameEventType_TurnStart {
        public GamePlayer Player;
    }

    public struct GameEventType_GameStartOver {
        public Room Room;
    }

    public struct GameEventType_GameBuildInMapScene {
        public GameRoom GameRoom;
    }
    
    //GameEventType结束后的通知
    public struct Wait_GameEventType_GameStart: IWaitType
    {
        public int Error
        {
            get;
            set;
        }
    }

    public struct Wait_GameEventType_CompletedAttack: IWaitType
    {
        public int Error
        {
            get;
            set;
        }
    }

    public struct Wait_GameEventType_GetHandCardsFromGroup: IWaitType
    {
        public int Error
        {
            get;
            set;
        }
    }

    public struct Wait_GameEventType_UnitRebuildFinish: IWaitType {
        public int Error { get; set; }
    }

    public struct Wait_GameEventType_RemoveCardFromGroup: IWaitType
    {
        public int Error
        {
            get;
            set;
        }
    }
}