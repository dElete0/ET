using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET {
    public class UIUnitShowHandler : MonoBehaviour {
        public static UIUnitShowHandler Instance;
        public static Camera _Camera;
        public static RectTransform Rect;
        public static bool IsFollow;
        public static CanvasGroup CanvasGroup;
        public static Action<bool> IsBeDrag;
        public static Transform Target;

        public void Awake() {
            Instance = this;
            _Camera = GameObject.Find("UICamera").GetComponent<Camera>();
            Rect = this.transform.parent.GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        public void Update() {
            if (IsFollow) {
                this.transform.position = Target.position;
            }
        }

        public static void Follow(float alpha, Transform transform) {
            IsBeDrag.Invoke(true);
            IsFollow = true;
            CanvasGroup.alpha = 1 - alpha;
            Target = transform;
        }

        public static void NotFollow() {
            IsBeDrag.Invoke(false);
            IsFollow = false;
        }
    }
}