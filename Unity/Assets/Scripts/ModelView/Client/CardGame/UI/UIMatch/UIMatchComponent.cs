using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(UI))]
    public class UIMatchComponent: Entity, IAwake
    {
        public GameObject NewGroupButton;
        public GameObject Group;
        public GameObject Next;
        public GameObject Last;
        public GameObject Groups;
        public GameObject Back;
        public GameObject Match;
    }
}