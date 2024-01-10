using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIHandCardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        //执行对目标释放的效果=>里头是发送消息to Server(仅Client内部调用)
        public Func<Vector3, bool> TryToDoInClient;
        //拖拽的动态效果
        public Func<Vector3, bool> IsToTargetInDrag;
        public Action<Vector3> CardPos;
        public Action<bool> IsBeDrag;
        public Action<bool> IsBeSelect;
        public Func<bool> IsDragEnable;
        public Action<bool> BeSelect;
        public bool IsMy;
        private float UI_Alpha = 1f;
        //鼠标位置
        Vector3 offset = Vector3.zero;
        public Vector3 GlobalMousePos;
        
        public static List<UIHandCardDragHandler> IsCardBeDrag = new List<UIHandCardDragHandler>();

        public Func<bool> CanBeUsed;
        public Func<bool> IsUnitInDrag;
        public Func<Vector3> GetTargetPos;
        public Func<Vector3> GetHeroVector;

        //需要移动物品的位置组件
        private RectTransform rt;
        private CanvasGroup CanvasGroup;

        //UI事件管理器
        private UnityEngine.EventSystems.EventSystem _EventSystem;
        private GraphicRaycaster gra;
        
        private const float HideDes = 100f;
        
        private void Awake() {
            rt = GetComponent<RectTransform>();
            _EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
        }

        private void Start() {
        }

        private void Update() {
            if (CanvasGroup == null)
            {
                return;
            }
 
            if (UI_Alpha != CanvasGroup.alpha) {
                CanvasGroup.alpha = UI_Alpha;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            BeSelect.Invoke(true);
            this.gameObject.transform.localScale = Vector3.one * 1.3f;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            BeSelect.Invoke(false);
            this.gameObject.transform.localScale = Vector3.one;
        }

        /// <summary>
        /// 开始拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (!this.CanBeUsed()) return;
            this.IsBeDrag.Invoke(true);
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.enterEventCamera, out Vector3 globalMousePos)) {
                // 计算偏移量
                offset = rt.position - globalMousePos;
                //rt.position = globalMousePos + offset;
            }
            IsCardBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (!this.CanBeUsed()) return;
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos)) {
                this.GlobalMousePos = globalMousePos;
                if (this.IsUnitInDrag.Invoke()) {
                    rt.position = globalMousePos + offset;
                    CardPos.Invoke(globalMousePos);
                } else {
                    if (this.IsToTargetInDrag.Invoke(globalMousePos)) {
                        float dis = (GetTargetPos.Invoke() - this.GlobalMousePos).magnitude;
                        this.UI_Alpha = dis > HideDes ? 0 : 1 - dis / HideDes;
                        UIArrowHandler.IsSetTarget = true;
                        UIArrowHandler.SetTarget(GetHeroVector.Invoke(), this.GlobalMousePos);
                    } else {
                        rt.position = globalMousePos + offset;
                    }
                }
            }
        }

        /// <summary>
        /// 结束拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData) {
            if (UIArrowHandler.IsSetTarget) {
                this.UI_Alpha = 1f;
                UIArrowHandler.IsSetTarget = false;
                this.rt.position = GetTargetPos.Invoke();
            }
            if (IsCardBeDrag.Contains(this)) IsCardBeDrag.Remove(this);
            this.IsBeDrag.Invoke(false);
            if (IsDragEnable.Invoke()) {
                if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
                    
                }
            }
            CardPos.Invoke(new Vector3(10000,0,0));
        }
    }
}

