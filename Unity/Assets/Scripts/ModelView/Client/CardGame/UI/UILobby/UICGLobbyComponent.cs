using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICGLobbyComponent: Entity, IAwake
    {
        public GameObject enterMap;
        public Text text;
        public Button replay;
        public InputField replayPath;
    }
}