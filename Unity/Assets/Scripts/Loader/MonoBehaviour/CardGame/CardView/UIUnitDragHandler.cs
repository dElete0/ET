using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIUnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        //仅Client内部调用
        public Func<Vector2, bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<Vector2> DragShow;
        public GameObject Target;
        public Action<bool> IsDrag;
        // CardId
        public long CardId;
        public int BaseId;
        public bool IsMy;

        public static List<UIUnitDragHandler> IsCardBeDrag = new List<UIUnitDragHandler>();

        public bool CanBeUsed = true;


        [Header("表示限制的区域")]
        public RectTransform LimitContainer;
        [Header("场景中Canvas")]
        public Canvas canvas;
        RectTransform rt;
        // 位置偏移量
        Vector3 offset = Vector3.zero;

        private Vector3 GlobalMousePos;
        //需要移动物品的位置组件
        private RectTransform rectTransform;

        //悬停展示
        private bool IsMouseEnter;
        private float MoustEnterTime;
        public Action ShowUIShowCard, HideUIShowCard;
        //UI事件管理器
        private UnityEngine.EventSystems.EventSystem _EventSystem;
        private GraphicRaycaster gra;
        
        private void Awake() {
            rt = GetComponent<RectTransform>();
            _EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
            rectTransform = GetComponent<RectTransform>();
        }

        private void Start() {
            this.canvas = this.transform.parent.parent.parent.parent.GetComponent<Canvas>();
        }
        
        private void Update() {
            if (this.IsMouseEnter) {
                this.MoustEnterTime += Time.deltaTime;
                if (this.MoustEnterTime > 1f) {
                    this.ShowUIShowCard.Invoke();
                    this.IsMouseEnter = false;
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            this.IsMouseEnter = true;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            this.IsMouseEnter = false;
            this.MoustEnterTime = 0f;
            this.HideUIShowCard.Invoke();
        }

        /// <summary>
        /// 开始拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (!this.CanBeUsed) return;
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.enterEventCamera, out Vector3 globalMousePos)) {
                // 计算偏移量
                offset = rt.position - globalMousePos;
            }
            IsDrag.Invoke(true);
            IsCardBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (!this.CanBeUsed) return;
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos)) {
                this.DragShow.Invoke(globalMousePos);
                this.GlobalMousePos = globalMousePos;
                UIArrowHandler.IsSetTarget = true;
                UIArrowHandler.SetTarget(this.rt.position, globalMousePos);
            }
        }
        

        /// <summary>
        /// 结束拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData) {
            //判断是否拖拽到目标上
            if (!IsMy) return;
            if (!this.CanBeUsed) return;
            UIArrowHandler.IsSetTarget = false;
            IsDrag.Invoke(false);
            if (IsCardBeDrag.Contains(this)) IsCardBeDrag.Remove(this);
            if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
            }
        }
    }
}