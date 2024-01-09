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
        public Action<GameObject> DragShow;
        public Action<Vector3> CardPos;
        public Action<bool> IsBeDrag;
        public Func<bool> IsDragEnable;
        public bool IsMy;
        //鼠标位置
        Vector3 offset = Vector3.zero;
        RectTransform rt;
        public Vector3 GlobalMousePos;

        public static GameObject ShowThis;
        [Header("场景中Canvas")]
        public Canvas canvas;
        public static List<UIHandCardDragHandler> IsCardBeDrag = new List<UIHandCardDragHandler>();

        public Func<bool> CanBeUsed;

        //需要移动物品的位置组件
        private RectTransform rectTransform;

        //UI事件管理器
        private UnityEngine.EventSystems.EventSystem _EventSystem;
        private GraphicRaycaster gra;
        
        private void Awake() {
            rt = GetComponent<RectTransform>();
            _EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
        }

        private void Start() {
            this.canvas = this.transform.parent.parent.parent.parent.GetComponent<Canvas>();
        }
        
        public void OnPointerEnter(PointerEventData eventData) {
            ShowThis = this.gameObject;
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            ShowThis = null;
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
            if (this.DragShow != null) this.DragShow.Invoke(null);
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos)) {
                this.GlobalMousePos = globalMousePos;
                rt.position = globalMousePos + offset;
            }
            CardPos.Invoke(globalMousePos);
        }

        /// <summary>
        /// 结束拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData) {
            //判断是否拖拽到目标上
            GameObject target = null;
            bool isUsed;
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

