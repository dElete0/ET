using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICGGameComponent: Entity, IAwake, IUpdate
    {
        public Text EnemyAccount;
        public GameObject TurnOver;

        public int DMyCost;
        public int DEnemyCost;
        public int DMyRed, DMyBlue, DMyWhite, DMyGreen, DMyBlack, DMyGrey;
        
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

        public UIUnitInfo MyHero;
        public UIUnitInfo MyAgent1, MyAgent2;

        public UIUnitInfo EnemyHero;
        public UIUnitInfo EnemyAgent1, EnemyAgent2;

        public Text MyTalkUI, EnemyTalkUI;
        public Sequence MyTalkSequence, EnemyTalkSequence;
        
        public GameObject MyGroup, EnemyGroup, GetHandCardShowPos;
        
        // 模板
        public GameObject UICard, UIUnit, UIEnemyHandCard;
        public UIUnitInfo UIShowCardInfo, UIUnitShowInfo;
        public GameObject HurtUI;
        
        // Select
        public GameObject UISelect;
        public List<UIUnitInfo> UISelectInfos = new List<UIUnitInfo>();

        //Deck
        public GameObject MyHandCardsDeck;
        public GameObject MyUnits;

        public GameObject EnemyHandCardsDeck;
        public GameObject EnemyUnits;

        public GameObject HurtUIs;

        public List<UIUnitInfo> MyHandCards = new List<UIUnitInfo>();
        public List<UIUnitInfo> EnemyHandCards = new List<UIUnitInfo>();
        public List<UIUnitInfo> MyFightUnits = new List<UIUnitInfo>();
        public List<UIUnitInfo> EnemyFightUnits = new List<UIUnitInfo>();
        public List<UIUnitInfo> HeroAndAgent = new List<UIUnitInfo>();

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
        public List<GameObject> HurtUIPool = new List<GameObject>();
        
        //玩家拖拽手牌的目标落点
        public int MyHandCardPos = -1;
        //玩家拖拽成功即将上场的手牌
        public UIUnitInfo MyHandCardUesd;
        //玩家选择的手牌
        public int SelectCardPos = -1;
        
        //常数
        public const float MyHandCardDes = 5f;
        public const float EnemyHandCardDes = 5f;
        public const float UnitsDes = 20f;
        public const float FindTarget = 5f;
        
        //动画执行期间的逻辑
        public bool IsGetHandCardAnim;
    }

    public enum TalkType {
        CantHaveMoreUnit = 0,
        CantDoNow = 1,
        CostNotEnough = 2
    }
}