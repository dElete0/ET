using System;

namespace ET.Server {
    //触发条件
    public struct TriggerEvent {
        /*public GameEventType GameEventType { private set; get; }
        //计数工具
        public int Count1, Count2, Count3;
        public long Actor, Target, Player;*/
        //触发条件
        public Func<GameEvent, bool> Triggeer;
        public TriggerEvent(Func<GameEvent, bool> triggeer) { this.Triggeer = triggeer; }
    }
    //事件
    public struct GameEvent {
        [StaticField]
        [ThreadStatic]
        public static GameEvent Instance;
        public bool IsDispose;//是否失效
        public GameEventType GameEventType;
        //触发此事件的事件， 事件链Info
        public Func<GameEvent, EventInfo, ETTask> ToDo;
        public int Count1, Count2, Count3;
        public long Actor, Target, Player;

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
        CallUnitOver = 117,
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
        
        AttackCountClear = 131,
        AllAttackCountClear = 132,
        SilentTarget = 133,
        AuraEffect = 134,
        AttributeAuraEffect = 135,
        
        AuraUnEffect = 136,
        AttributeAuraUnEffect = 137,
        AuraEffectToTarget = 138,
        AttributeAuraEffectToTarget = 139,
        KillTargetUnit = 140,
        
        KillAllUnit = 141,
        FindAndCloneCard = 142,
        GetQualifications = 143,
        GetArmor = 144,
        TargetGetPower = 145,
        
        RemoveTargetUnit = 146,
        RemoveUnits = 147,
        TreatTarget = 148,
        AddCardToGroupByBaseId = 149,
        SwapArmor = 150,
        
        TargetGetAttribute = 151,
        GoldenShip = 152,
        PowerToUseCard = 153,
        TargetGetAttackThisTurn = 154,
        LoseAttack = 155,
        
        UnitsGetAttribute = 156,
        UnitsInGroupGetAttribute = 157,
        UnitsInGroupLoseAttributeAddDamageEnemyHero = 158,
        TargetBackToHandCards = 159,
        TargetBackToGroup = 160,
        
        AddTargetCardToGroup = 161,
        Erosion = 162,
        MyHeroGetTargetPowerThisTurn = 163,
        TargetLosePower = 164,
    }
}