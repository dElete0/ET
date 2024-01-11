using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET {
    public class UIArrowHandler : MonoBehaviour {
        public static UIArrowHandler Instance;
        public static RectTransform Body;
        public static bool IsSetTarget;
        public static bool IsFindTarget;
        public static Transform Begin;
        public static Camera _Camera;
        public static RectTransform Rect;

        public void Awake() {
            Instance = this;
            Body = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
            _Camera = GameObject.Find("UICamera").GetComponent<Camera>();
            Rect = this.transform.parent.GetComponent<RectTransform>();
        }

        public void Update() {
            if (IsSetTarget) return;
            if (IsFindTarget) {
                Vector3 worldpos;
                RectTransformUtility.ScreenPointToWorldPointInRectangle(Rect, Input.mousePosition, _Camera, out worldpos);
                SetTarget(Begin.position, worldpos);
            } else {
                Instance.transform.position = new Vector3(-999999, -999999);
            }
        }

        public static void SetTarget(Vector3 begin, Vector3 target) {
            Instance.transform.position = target;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, target - begin);
            Instance.transform.rotation = rotation;
            Body.localScale = new Vector3(1, (target - begin).magnitude * 10f, 1);
        }
    }
}