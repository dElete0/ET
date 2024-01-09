using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ChildOf(typeof(UIAnimComponent))]
    public class UIUnitInfo : Entity, IAwake<GameObject>, IUpdate {
        public GameObject CardGo;
        public Vector3 TargetPos;

        public long CardId;
        public int BaseId;
        //参数
        public int DCost;
        public int DRed;
        public int DGreen;
        public int DBlue;
        public int DGrey;
        public int DBlack;
        public int DWhite;
        public int DAttack;
        public int DHP;
        
        // UI
        public Text Name, Info;
        public Image Image;

        public Text Cost;

        public Text Red;
        public Text Green;
        public Text Blue;
        public Text Grey;
        public Text Black;
        public Text White;

        public Text Attack { set; get; }
        public Text HP;

        public GameObject Taunt;
        
        //使用方式等
        public CardType CardType;
        public UseCardType UseCardType;

        //动画机是否在移动
        public bool IsMove;
        public bool IsDrag;
    }
}