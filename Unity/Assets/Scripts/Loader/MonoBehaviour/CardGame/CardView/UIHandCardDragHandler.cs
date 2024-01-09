using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIHandCardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        //执行对目标释放的效果=>里头是发送消息to Server
        public Action<long, int> UseCardToServer;
        //仅Client内部调用
        public Func<Vector3, bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<GameObject> DragShow;
        public Action<Vector3> CardPos;
        // CardId
        public long CardId;
        public int BaseId;
        public bool IsMy;
        //鼠标位置
        Vector3 offset = Vector3.zero;
        RectTransform rt;
        private Vector3 GlobalMousePos;

        public static GameObject ShowThis;
        [Header("场景中Canvas")]
        public Canvas canvas;
        public static List<UIHandCardDragHandler> IsCardBeDrag = new List<UIHandCardDragHandler>();

        public Func<bool> CanBeUsed;
        [FormerlySerializedAs("UseCardType")]
        public UIUseCardType UIUseCardType;
        [FormerlySerializedAs("CardType")]
        public UICardType UICardType;
        //记录玩家开始拖拽时的位置
        private Vector3 vector;

        //需要移动物品的位置组件
        private RectTransform rectTransform;

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
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.enterEventCamera, out Vector3 globalMousePos)) {
                // 计算偏移量
                offset = rt.position - globalMousePos;
                rt.position = globalMousePos + offset;
            }
            vector = this.transform.position;
            IsCardBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            if (!IsMy) return;
            if (this.DragShow != null) this.DragShow.Invoke(null);
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos)) {
                this.GlobalMousePos = globalMousePos;
                rt.position = globalMousePos + offset;
            }
            if (this.UICardType == UICardType.Unit) CardPos.Invoke(globalMousePos);
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

            if ((int)this.UICardType == (int)UICardType.Magic && (int)this.UIUseCardType == (int)UIUseCardType.NoTarget) {
                Log.Warning("打出的距离是否足够远");
                //打出的距离是否足够远
                if(this.transform.position.y > this.vector.y / 2f) {
                    if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
                    
                    } else{
                        this.rectTransform.position = vector;
                    }
                }
            } else if ((int)this.UICardType == (int)UICardType.Unit && (int)this.UIUseCardType == (int)UIUseCardType.NoTarget) {
                if(this.transform.position.y > this.vector.y / 2f) {
                    if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
                    
                    } else{
                        this.rectTransform.position = vector;
                    }
                }
            } else if ((int)this.UICardType == (int)UICardType.Magic && (int)this.UIUseCardType == (int)UIUseCardType.ToUnit) {
                if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
                    
                } else{
                    this.rectTransform.position = vector;
                }
            }
            CardPos.Invoke(new Vector3(10000,0,0));
        }
    }
        
    public enum UIUseCardType {
        NoTarget,
        ToActor,
        ToUnit,
        ToHero,
        ToMyHero,
        ToMyUnit,
        ToEnemyHero,
        ToEnemyUnit,
    }

    public enum UICardColor {
        none = 0,
        red = 1,
        green = 2,
        blue = 3,
        black = 4,
        white = 5,
        gruy = 6,
    }

    public enum UICardPos {
        None = 0,//墓地，未加入手牌等
        Hand = 1,//手牌
        Group = 2,
    }

    public enum UICardType {
        Hero = 1,
        Legend = 2,
        Unit = 3,
        Magic = 4,
        Star = 5,
        Disaster = 6,
        Plot = 7,
        Weapon = 8,
        Building = 9,
        Agent = 10,
    }
}

