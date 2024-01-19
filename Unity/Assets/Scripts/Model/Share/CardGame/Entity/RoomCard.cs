using System.Collections.Generic;
using ET.Server;

namespace ET {
    [ChildOf(typeof(CardGameComponent_Cards))]
    //游戏内的卡牌
    public class RoomCard : Entity, IAwake<int, long> {
        public int ConfigId { get; set; } //配置表id
        public CardType CardType;
        public CardUnitType UnitType;
        public string Name;

        public long PlayerId;

        //基础属性
        public int HPD;//默认血量
        public int HPMax;//最大血量
        public int HP;//当前血量
        public int Attack;
        public int AttackD;
        public int Armor;
        
        //提供的资质
        public (CardColor, CardColor) Colors;
        
        //资质需求
        public int Red;
        public int RedD;
        public int Green;
        public int GreenD;
        public int Blue;
        public int BlueD;
        public int Black;
        public int BlackD;
        public int White;
        public int WhiteD;
        public int Grey;
        public int GreyD;
        
        //费用需求
        public int Cost;
        public int CostD;

        //使用方式
        public UseCardType UseCardType;
        public bool CantBeAttacTarget;
        public bool CantBeMagicTarget;

        //属性异能
        public List<Power_Type> AttributePowers = new List<Power_Type>();
        //其他异能
        public List<Power_Struct> OtherPowers = new List<Power_Struct>();
        //身上的光环效果
        public List<Power_Struct> AuraOnThisPowers = new List<Power_Struct>();
        
        //其他计数
        //刚上场，不能攻击
        public bool IsCallThisTurn;
        //攻击次数计数
        public int AttackCount;
        public int AttackCountMax = 1;
        //死亡标记
        public bool IsDeadState;
    }

    //部署效果及计数器
    public struct Power_Struct {
        public Power_Type PowerType;
        public TriggerPowerType TriggerPowerType;
        public int Count1, Count2, Count3;
        public GameEvent TriggerEvent;

        public Power_Struct(Power_Struct basePower, int type, int count) {
            this.PowerType = basePower.PowerType;
            this.TriggerEvent = basePower.TriggerEvent;
            this.TriggerPowerType = basePower.TriggerPowerType;
            this.Count1 = basePower.Count1;
            this.Count2 = basePower.Count2;
            this.Count3 = basePower.Count3;
            switch (type) {
                case 1:
                    this.Count1 += count;
                    break;
                case 2:
                    this.Count2 += count;
                    break;
                case 3:
                    this.Count3 += count;
                    break;
            }
        }
    }

    // 触发方式
    public enum TriggerPowerType {
        //属性
        Attribute = 0,
        //部署
        Arrange = 1,
        //亡语
        Dead = 2,
        //光环
        Aura = 3,
        //游戏开始时触发
        GameStart = 4,
        //抽到时释放
        WhenGetHandCard = 5,
        //立刻释放（法术/战术）
        Release = 6,
        //监听器触发
        Monitor = 7,
    }

    public enum UseCardType {
        NoTarget = 0,
        ToActor = 1,
        ToUnit = 2,
        ToHero = 3,
        ToAgent = 4,
        ToMyUnit = 5,
        ToMyAgent = 6,
        ToEnemyUnit = 7,
        ToEnemyAgent = 8,
        ToMyActor = 9,
        ToEnemyActor = 10,
    }

    public enum CardColor {
        None = 0,
        Red = 1,
        Green = 2,
        Blue = 3,
        Black = 4,
        White = 5,
        Grey = 6,
    }

    public enum CardPos {
        None = 0,//墓地，未加入手牌等
        Hand = 1,//手牌
        Group = 2,
    }

    /// <summary>
    /// Type
    /// 1:英雄
    /// 2:传奇
    /// 3.单位
    /// 4.魔法
    /// 5.星典
    /// 6.灾难
    /// 7.战术
    /// 8.武器
    /// 9.建筑
    /// 10.干员
    /// </summary>
    public enum CardType {
        Hero = 1,
        Legend = 2,
        Unit = 3,
        Magic = 4,
        Star = 5,
        Disaster = 6,
        Plot = 7,
        Weapon = 8,
        Building = 9,
        Agent = 10,
        ExclusionZone = 11,
    }
    
    public enum CardUnitType {
        None = 0,
        Legend = 1,
        Nomal = 2,
        God = 3,
        Star = 5,
        Building = 9,
        ExclusionZone = 11,
    }

    public enum CallType {
        Nomal = 0,
        RedDragon = 1,
    }

    //卡牌效果种类
    public enum Power_Type {
        None = 0,
        //冲锋
        Charge = 101,
        //一回合攻击两次
        AttackTwice = 102,
        //突袭
        Rush = 103,
        // 免疫
        Immunity = 106,
        // 英雄免疫
        HeroImmunity = 107,
        // 嘲讽
        Taunt = 108,
        // 不能攻击英雄
        CantAttackHeroAndAgent = 109,
        // 风险
        Risk = 110,
        // 泡泡
        Bubbles = 111,
        // 传承
        Inherit = 112,
        
        
        //每当你打出一张费用大于3的法术牌，获得1点攻击力
        GetAttackByUseMagci = 1001,
        // 你获得一个额外回合
        GetAnotherTurn = 1002,
        // 你的所有单位获得免疫
        YourAllUnitsGetImmunity = 1003,
        // 随机释放n个领域法术
        RelesaseAreaMagic = 1004,
        // 移除你对手的所有单位
        RemoveAllEnmeyUnits = 1005,
        // 你的对手无法拥有单位
        EnemyCantGetUnit = 1006,
        // 召唤指定单位
        CallTargetUnit = 1007,
        // 你的所有单位获得冲锋
        YourAllUnitsGetCharge = 1008,
        // 法术增强
        AddMagicDamage = 1009,
        // 你的法术不消耗资源
        MagicNoCost = 1010,
        // 将指定干员加入战场
        LetAgentAdd = 1011,
        // 抽牌
        GetHandCardFromGroup = 1012,
        //对目标造成伤害
        DamageHurt = 1013,
        //亵渎
        Desecrate = 1014,
        //属性光环
        AttributeAura = 1015,
        //对所有单位造成伤害
        DamageAllUnit = 1016,
        //对目标单位沉默
        SilentTarget = 1017,
        //侵蚀
        Erosion = 1018,
        //召唤红龙
        CallRedDragon = 1019,
        //消灭目标单位
        KillTargetUnit = 1020,
        //消灭所有单位
        KillAllUnit = 1021,
        //发现并复制目标来源的中的n张卡
        FindAndCloneCard = 1022,
        //获得资质
        GetQualifications = 1023,
        //获得护甲
        GetArmor = 1024,
        //目标获得异能
        TargetGetPower = 1025,
        //移除目标单位
        RemoveTargetUnit = 1026,
        //治疗目标角色
        TreatTarget = 1027,
        //向牌库增加目标卡牌(显式)
        AddCardToGroupShow = 1028,
        //向牌库增加目标卡牌(隐式)
        AddCardToGroupHide = 1029,
        //交换双方护甲
        SwapArmor = 1030,
        //目标单位获得属性
        TargetGetAttribute = 1031,
        //跨越时代的黄金船
        GoldenShip = 1032,
        //打出一张牌
        PowerToUseCard = 1033,
    }
}