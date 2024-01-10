using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET {
    public class UIArrowHandler : MonoBehaviour {
        public static UIArrowHandler Instance;
        public static RectTransform Body;
        public static bool IsSetTarget;

        public void Awake() {
            Instance = this;
            Body = this.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
        }

        public void Update() {
            if (IsSetTarget) return;
            Instance.transform.position = new Vector3(-999999, -999999);
        }

        public static void SetTarget(Vector3 begin, Vector3 target) {
            Instance.transform.position = target;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.down, target - begin);
            Instance.transform.rotation = rotation;
            Body.localScale = new Vector3(1, (target - begin).magnitude * 10f, 1);
        }
    }
}