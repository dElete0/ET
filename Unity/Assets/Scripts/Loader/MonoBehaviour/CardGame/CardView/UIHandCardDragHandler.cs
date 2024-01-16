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
        public Func<bool> TryToDoInClient;
        public Func<bool> SetUnitTargetToDo;
        public Action CancelTarget;
        //拖拽的动态效果
        public Func<Vector3, bool> IsToTargetInDrag;
        public Action<Vector3> SetTarget;
        public Action<Vector3> CardPos;
        public Action<bool> IsBeDrag;
        public Action<bool> IsBeSelect;
        public Func<bool> IsDragEnable;
        public Action<bool> BeSelect;
        public bool IsMy;
        private float UI_Alpha = 1f;
        private bool IsFindTarget;
        //鼠标位置
        Vector3 offset = Vector3.zero;
        public Vector3 GlobalMousePos;
        
        public static List<UIHandCardDragHandler> IsCardBeDrag = new List<UIHandCardDragHandler>();

        public Func<bool> CanBeUsed;
        public Func<bool> IsGetHandCardAnim;
        public Func<bool> IsUnitInDrag;
        public Action SetUIUnitShow;
        public Func<Vector3> GetTargetPos;
        public Func<Vector3> GetHeroVector;

        //需要移动物品的位置组件
        private RectTransform rt;
        private CanvasGroup CanvasGroup;

        private const float HideDes = 30f;
        
        private void Awake() {
            rt = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start() {
        }

        private void Update() {
            if (this.IsFindTarget) {
                if (Input.GetMouseButtonDown(0)) {
                    this.OnSetTarget();
                } else if (Input.GetMouseButtonDown(1)) {
                    this.CanelTarget();
                } else {
                    this.OnFindTarget();
                }
            }
            if (UI_Alpha != CanvasGroup.alpha) {
                CanvasGroup.alpha = UI_Alpha;
            }
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (IsCardBeDrag.Count > 0) return;
            if (IsGetHandCardAnim.Invoke()) return;
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
            if (this.IsUnitInDrag.Invoke()) {
                SetUIUnitShow.Invoke();
            }
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
                    //单位牌
                    rt.position = globalMousePos + offset;
                    CardPos.Invoke(globalMousePos);
                    float dis = (GetTargetPos.Invoke() - this.GlobalMousePos).magnitude;
                    this.UI_Alpha = dis > HideDes ? 0 : 1 - dis / HideDes;
                    UIUnitShowHandler.Follow(this.UI_Alpha, this.transform);
                } else {
                    if (this.IsToTargetInDrag.Invoke(globalMousePos)) {
                        //具有目标的法术牌
                        float dis = (GetTargetPos.Invoke() - this.GlobalMousePos).magnitude;
                        this.UI_Alpha = dis > HideDes ? 0 : 1 - dis / HideDes;
                        UIArrowHandler.IsSetTarget = true;
                        UIArrowHandler.SetTarget(GetHeroVector.Invoke(), this.GlobalMousePos);
                    } else {
                        //无目标的法术牌
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
            this.UI_Alpha = 1f;
            if (UIArrowHandler.IsSetTarget) {
                UIArrowHandler.IsSetTarget = false;
                this.rt.position = GetTargetPos.Invoke();
            }
            if (IsCardBeDrag.Contains(this)) IsCardBeDrag.Remove(this);
            this.IsBeDrag.Invoke(false);
            if (IsDragEnable.Invoke()) {
                if (this.TryToDoInClient.Invoke()) {
                    //如果是需要二次选择目标的卡牌
                    IsFindTarget = true;
                    this.UI_Alpha = 0f;
                    UIArrowHandler.Begin = this.transform;
                    UIArrowHandler.IsFindTarget = true;
                } else {
                    UIUnitShowHandler.NotFollow();
                    CardPos.Invoke(new Vector3(10000,0,0));
                }
            } else {
                UIUnitShowHandler.NotFollow();
                CardPos.Invoke(new Vector3(10000,0,0));
            }
        }

        private void OnFindTarget() {
            SetTarget.Invoke(UIArrowHandler.Instance.transform.position);
        }

        private void OnSetTarget() {
            IsFindTarget = false;
            UIArrowHandler.IsFindTarget = false;
            UIUnitShowHandler.NotFollow();
            this.UI_Alpha = 1f;
            if (SetUnitTargetToDo.Invoke()) {
                CardPos.Invoke(new Vector3(10000,0,0));
            } else {
                this.CancelTarget.Invoke();
            }
        }
        
        private void CanelTarget() {
            UIUnitShowHandler.NotFollow();
            this.UI_Alpha = 1f;
            UIArrowHandler.IsFindTarget = false;
            this.CancelTarget.Invoke();
            CardPos.Invoke(new Vector3(10000,0,0));
        }
    }
}

