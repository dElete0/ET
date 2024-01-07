using System.Collections.Generic;

namespace ET {
    [ChildOf(typeof(CardGameComponent_Cards))]
    //游戏内的卡牌
    public class RoomCard : Entity, IAwake<int> {
        public int ConfigId { get; set; } //配置表id
        public CardType CardType;

        public long PlayerId;

        //基础属性
        public int HPD;//默认血量
        public int HPMax;//最大血量
        public int HP;//当前血量
        public int Attack;
        public int AttackD;
        
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

        public int AttackCount;
        public int AttackCountMax = 1;

        //属性异能
        public List<Power_Type> AttributePowers = new List<Power_Type>();
        //其他异能
        public List<Power_Struct> OtherPowers = new List<Power_Struct>();
    }

    //部署效果及计数器
    public struct Power_Struct {
        public Power_Type PowerType;
        public TriggerPowerType TriggerPowerType;
        public int Count1, Count2, Count3;
        public long Card1, Card2, Card3;
        public long RoomPlayer1, RoomPlayer2;
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
        Effect = 3,
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
        ToMyHero = 5,
        ToMyUnit = 6,
        ToMyAgent = 7,
        ToEnemyHero = 8,
        ToEnemyUnit = 9,
        ToEnemyAgent = 10,
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
    }
}