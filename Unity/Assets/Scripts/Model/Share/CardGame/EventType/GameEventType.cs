namespace ET {

    public struct GameEventType_GameStart {
        public GameRoom room;
    }
    
    public struct GameEventType_Attack {
        public GameCard Actor;
        public GameCard Target;
    }

    public struct GameEventType_GetHandCardsFromGroup {
        public GamePlayer Target;
        public GameRoom Room;
        public Component_Card Cards;
        public int Count;
    }

    public struct GameEventType_GetHandCard {
        public GameCard Card;
        public Component_Player_HandCards HandCards;
    }

    public struct GameEventType_RemoveCardFromGroup {
        public GameCard Card;
        public Component_Player_Group Group;
    }

    public struct GameEventType_TurnOver {
        public GamePlayer Player;
    }

    public struct GameEventType_TurnStart {
        public GamePlayer Player;
    }

    public struct GameEventType_GameStartOver {
        public GameRoom room;
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