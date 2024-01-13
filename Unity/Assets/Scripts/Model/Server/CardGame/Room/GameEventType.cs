using System;

namespace ET.Server {
    //触发条件
    public class TriggerEvent {
        public GameEventType GameEventType { private set; get; }
        //计数工具
        public int Count1, Count2, Count3;
        public long Id1, Id2;
        //触发条件
        public Func<GameEvent, bool> Triggeer;
        //失效条件
        public Func<GameEvent, bool> Disponse;
        public TriggerEvent(Func<GameEvent, bool> triggeer) { this.Triggeer = triggeer; }
    }
    //事件
    public class GameEvent {
        public bool IsDispose;//是否失效
        public GameEventType GameEventType;
        public Action<EventInfo> ToDo;
        public int Count1, Count2, Count3;
        public long Id1, Id2, Id3;

        public GameEvent(GameEventType type) {
            this.GameEventType = type;
        }
    }

    public enum GameEventType {
        None = 0,
        
        //Base
        GetCardsFromGroup = 101,
        GetHandCard = 102,
        RemoveCardFromGroup = 103,
        GetCardFromGroup = 104,
        
        UseCard = 105,
        AttackTo = 106,
        Damage = 107,
        DamageHero = 108,
        Destory = 109,
        
        TurnOver = 110,
        TurnStart = 111,
        UseUnitCard = 112,
        UseMagicCard = 113,
        
        CallUnit = 114,
        UnitArrange = 115,
        Dead = 116,
        UnitBeCalled = 117,
        GetCostTotal = 118,
        ResetCost = 119,
        AttackOver = 120,
        
        GetBaseColor = 121,
        UsePlotCard = 122,
        MagicTakesEffect = 123,
        PlotTakesEffect = 124,
        DeadOver = 125,
        Desecrate = 126,
        DamageAllUnit = 127,
        DamageAllActor = 128,
        DamageAllEnemyUnit = 129,
        DamageAllMyUnit = 130,
    }
}