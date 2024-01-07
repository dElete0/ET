using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICardComponent: Entity, IAwake {
        public Text Name, Info;
        public Image Image;

        public Text Cost;

        public Text Red;
        public Text Green;
        public Text Blue;
        public Text Grey;
        public Text Black;
        public Text White;

        public Text Attack;
        public Text HP;
    }
}