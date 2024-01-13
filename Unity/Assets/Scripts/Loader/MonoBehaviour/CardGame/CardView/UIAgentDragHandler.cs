using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIAgentDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        //仅Client内部调用
        public Func<bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<Vector2> DragShow;
        public Action<bool> IsDrag;
        public Func<bool> CanBeUsed;
        // CardId
        public bool IsMy;
        public static List<UIAgentDragHandler> IsAgentBeDrag = new List<UIAgentDragHandler>();

        RectTransform rt;

        private Vector3 GlobalMousePos;

        //悬停展示
        private bool IsMouseEnter;
        private float MoustEnterTime;
        public Action ShowUIShowCard, HideUIShowCard;
        
        private void Awake() {
            rt = GetComponent<RectTransform>();
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
            if (!this.CanBeUsed.Invoke()) return;
            IsDrag.Invoke(true);
            IsAgentBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (!this.CanBeUsed.Invoke()) return;
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
            if (!this.CanBeUsed.Invoke()) return;
            UIArrowHandler.IsSetTarget = false;
            IsDrag.Invoke(false);
            if (IsAgentBeDrag.Contains(this)) IsAgentBeDrag.Remove(this);
            if (this.TryToDoInClient.Invoke()) {
            }
        }
    }
}