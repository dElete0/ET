using UnityEngine;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class CardPresenterComponent: Entity, IAwake, IUpdate
    {
        public Transform HandLayoutRoot;
        public Transform GroundLayoutRoot;

        public GameObject CardViewPrefab;
        public List<CardViewComponent> CardViews = new List<CardViewComponent>();
    }
}