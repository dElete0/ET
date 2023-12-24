using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UIMainComponent : Entity, IAwake
    {
        public GameObject FindEnemy;
        public GameObject FightWithAi;
        public Text text;
    }
}