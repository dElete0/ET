using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICGGroupComponent: Entity, IAwake {
        public GameObject BuildOver;
        public GameObject UIGroupAgent1;
        public GameObject UIGroupAgent2;
        public GameObject UIGroupHero;
        public GameObject Layout;
        public GameObject UICard1, UICard2, UICard3, UICard4, UICard5, UICard6, UICard7, UICard8, UICard9, UICard10;
        public GameObject Last, Next;
        public GameObject Grey, Black, Green, White, Blue, Red;
        public GameObject Hero, Agent;
    }
}