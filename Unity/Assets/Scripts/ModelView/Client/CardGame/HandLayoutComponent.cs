using UnityEngine;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(CardPresenterComponent))]
    public class HandLayoutComponent: Entity, IAwake, IUpdate
    {
        public GameObject selfObject;
        
        public int HandCount = 0;
        public List<Vector3> TargetPositions = new List<Vector3>();
        public List<Quaternion> TargetRotations = new List<Quaternion>();
        public List<Transform> CardTransforms = new List<Transform>();

        public float anchoredCenterY = -40;
        public float anchoredDeltaAngle = 1.5f;
        public float anchoredRadius = 40f;

        public float lerpTimer = 0;
        public float lerpTransformValue = 0.15f;
        public float lerpScaleValue = 0.1f;
    }
}