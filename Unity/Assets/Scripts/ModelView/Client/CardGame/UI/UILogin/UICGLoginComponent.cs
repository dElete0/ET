using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UICGLoginComponent: Entity, IAwake
    {
        public GameObject account;
        public GameObject password;
        public GameObject loginBtn;
    }
}