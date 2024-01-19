using System.Collections.Generic;

namespace ET.Client {
    public struct SceneChangeStart {
    }

    public struct RoomChangeStart {

    }

    public struct SceneChangeFinish {
    }

    public struct RoomChangeFinish {

    }

    public struct AfterCreateClientScene {
    }

    public struct AfterCreateCurrentScene {
    }

    public struct AppStartInitFinish {
    }

    public struct LoginFinish {
    }

    public struct EnterRoom {
    }

    public struct FindEnemy {

    }

    public struct FightWithAi {
    }

    public struct EnterMapFinish {
    }

    public struct AfterUnitCreate {
        public Unit Unit;
    }

    public struct EnemyGroupCountType {
        public int Count;
    }

    public struct EnemyNewAgentType {
        public RoomCardInfo Agent1;
        public RoomCardInfo Agent2;
    }

    public struct EnemyNewHeroType {
        public RoomCardInfo Hero;
    }

    public struct FindCardsToShow {
        public List<RoomCardInfo> Cards;
    }

    public struct GetColor {
        public CardColor Color;
        public int Num;
        public bool IsMy;
    }

    public struct GetArmor {
        public int Num;
        public bool IsMy;
    }

    public struct FlashMyUnits {
        public List<RoomCardInfo> Cards;
    }

    public struct FlashEnemyUnits {
        public List<RoomCardInfo> Cards;
    }

    public struct FlashUnit {
        public RoomCardInfo Card;
    }

    public struct GroupCountType {
        public int Count;
    }

    public struct GetHandCardsFromGroup {
        public List<RoomCardInfo> Cards;
    }
    
    public struct EnemyGetHandCardsFromGroup {
        public List<RoomCardInfo> Cards;
    }

    public struct NewAgentType {
        public RoomCardInfo Agent1;
        public RoomCardInfo Agent2;
    }

    public struct NewHero {
        public RoomCardInfo Hero;
    }

    public struct MyCost {
        public int Cost;
        public int CostMax;
    }

    public struct EnemyCost {
        public int Cost;
        public int CostMax;
    }

    public struct SetAgent1 {
        public RoomCardInfo Agent;
    }

    public struct SetAgent2 {
        public RoomCardInfo Agent;
    }

    public struct TurnStart {
        public bool IsThisClient;
        public int Cost;
        public int CostD;
        public int Red;
        public int Blue;
        public int Green;
        public int White;
        public int Black;
        public int Grey;
    }

    public struct UnitDead {
        public List<long> CardIds;
    }

    public struct GetHandCardFromGroup {
        public RoomCardInfo Card;
    }

    public struct RiskSuccess {
        public RoomCardInfo Card;
        public bool IsSuccess;
    }

    public struct ShowUseCard {
        public RoomCardInfo Card;
        public bool IsMy;
    }

    public struct TreatTergets {
        public List<RoomCardInfo> CardInfos;
        public List<int> Nums;
    }

    public struct GetHandCards {
        public List<RoomCardInfo> Cards;
    }

    public struct EnemyGetHandCards {
        public List<RoomCardInfo> Cards;
    }

    public struct LoseHandCard {
        public long CardId;
    }

    public struct EnemyGetHandCardFromGroup {
        public RoomCardInfo Card;
    }

    public struct CallUnit {
        public RoomCardInfo Card;
        public List<long> UnitsOrder;
    }
    
    public struct EnemyCallUnit {
        public RoomCardInfo Card;
        public List<long> UnitsOrder;
    }
    
    public struct CallUnits {
        public List<RoomCardInfo> Card;
        public List<long> UnitsOrder;
    }
    
    public struct EnemyCallUnits {
        public List<RoomCardInfo> Card;
        public List<long> UnitsOrder;
    }

    public struct CardGetDamage {
        public RoomCardInfo Card;
        public int Hurt;
    }

    public struct CardsGetDamage {
        public List<RoomCardInfo> Card;
        public List<int> Hurt;
    }

    public struct RoomCardAttack {
        public long ActorId;
        public long TargetId;
    }

    public struct AddCardsToGroupShow {
        public List<RoomCardInfo> Cards;
        public bool IsMy;
    }
    
    public struct AddCardsToGroupHide {
        public int Num;
        public bool IsMy;
    }
}