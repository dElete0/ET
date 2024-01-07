using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICGGameComponent: Entity, IAwake, IUpdate
    {
        public Text EnemyAccount;
        public GameObject TurnOver;

        public Text MyCost;
        public Text EnemyCost;

        public Text MyGroupCount;
        public Text EnemyGroupCount;

        public Text MyRed;
        public Text MyGreen;
        public Text MyBlue;
        public Text MyGrey;
        public Text MyBlack;
        public Text MyWhite;
        
        public Text EnemyRed;
        public Text EnemyGreen;
        public Text EnemyBlue;
        public Text EnemyGrey;
        public Text EnemyBlack;
        public Text EnemyWhite;

        public GameObject MyHero;
        public GameObject MyAgent1, MyAgent2;

        public GameObject EnemyHero;
        public GameObject EnemyAgent1, EnemyAgent2;
        
        // 模板
        public GameObject UICard, UIUnit, UIEnemyHandCard;
        public UIUnitInfo UIShowCardInfo;
        
        //Deck
        public GameObject MyHandCardsDeck;
        public GameObject MyUnits;

        public GameObject EnemyHandCardsDeck;
        public GameObject EnemyUnits;

        public Dictionary<RoomCardInfo, UIUnitInfo> MyHandCards = new Dictionary<RoomCardInfo, UIUnitInfo>();
        public Dictionary<RoomCardInfo, UIUnitInfo> EnemyHandCards = new Dictionary<RoomCardInfo, UIUnitInfo>();
        public Dictionary<RoomCardInfo, UIUnitInfo> MyFightUnits = new Dictionary<RoomCardInfo, UIUnitInfo>();
        public Dictionary<RoomCardInfo, UIUnitInfo> EnemyFightUnits = new Dictionary<RoomCardInfo, UIUnitInfo>();
        public Dictionary<RoomCardInfo, UIUnitInfo> HeroAndAgent = new Dictionary<RoomCardInfo, UIUnitInfo>();

        //回合开始
        public GameObject TurnStart;
        public bool IsShowTurnStart;
        public bool IsWaitCloseTurnStart;
        public long ShowTurnStartTime;
        public const long ShowTunStartTimeD = 1500;

        //其他
        public Button Setting;
        public Button Run;
        
        //对象池
        public List<GameObject> UnitPool = new List<GameObject>();
        public List<GameObject> MyHandCardPool = new List<GameObject>();
        public List<GameObject> EnemyHandCardPool = new List<GameObject>();
    }
}