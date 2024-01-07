using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIHandCardDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        //执行对目标释放的效果=>里头是发送消息to Server
        public Action<long, int> UseCardToServer;
        //仅Client内部调用
        public Func<float, bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<GameObject> DragShow;
        // CardId
        public long CardId;

        public static List<UIHandCardDragHandler> IsCardBeDrag = new List<UIHandCardDragHandler>();

        public bool CanBeUsed = true;
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
            _EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
            rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 开始拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnBeginDrag(PointerEventData eventData) {
            vector = this.transform.position;
            IsCardBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            //if (!this.CanBeUsed) return;
            if (this.DragShow != null) this.DragShow.Invoke(null);
            this.rectTransform.anchoredPosition += eventData.delta;
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
                    if (this.TryToDoInClient.Invoke(0f)) {
                    
                    } else{
                        this.rectTransform.position = vector;
                    }
                }
            } else if ((int)this.UICardType == (int)UICardType.Unit && (int)this.UIUseCardType == (int)UIUseCardType.NoTarget) {
                if(this.transform.position.y > this.vector.y / 2f) {
                    if (this.TryToDoInClient.Invoke(0f)) {
                    
                    } else{
                        this.rectTransform.position = vector;
                    }
                }
            } else if ((int)this.UICardType == (int)UICardType.Magic && (int)this.UIUseCardType == (int)UIUseCardType.ToUnit) {
                if (this.TryToDoInClient.Invoke(0f)) {
                    
                } else{
                    this.rectTransform.position = vector;
                }
            }
        }

        public long GetTarget() {
            var list = GraphicRaycaster(Input.mousePosition);

            foreach (var goGraph in list) {
                //检测是否再目标上
                if (goGraph.gameObject.tag.Equals("target")) {
                    // todo 返回一个值
                    return 0;
                }
            }

            return 0;
        }



        /// <summary>
        /// 定义通过射线读取所在位置的UI对象
        /// </summary>
        /// <param name="pos">射线位置</param>
        /// <returns>返回读取的所有UI对象</returns>
        private List<RaycastResult> GraphicRaycaster(Vector2 pos)
        {
            var mPointerEventData = new PointerEventData(_EventSystem);
            mPointerEventData.position = pos;
            List<RaycastResult> results = new List<RaycastResult>();
            gra.Raycast(mPointerEventData, results);
            return results;
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

