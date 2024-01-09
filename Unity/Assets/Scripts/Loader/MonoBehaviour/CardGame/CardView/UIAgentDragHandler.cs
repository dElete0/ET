using System;
using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIAgentDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler {
        //执行对目标释放的效果=>里头是发送消息to Server
        public Action<long> UseCardToServer;
        //仅Client内部调用
        public Func<bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<GameObject> DragShow;
        // CardId
        public long CardId;
        public int BaseId;
        public bool IsMy;
        public static bool IsCardBeDrag;

        public bool CanBeUsed = true;
        [FormerlySerializedAs("UseCardType")]
        public UIUseCardType UIUseCardType;
        [FormerlySerializedAs("CardType")]
        public UICardType UICardType;
        //记录玩家开始拖拽时的位置
        private Vector3 vector;

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
            _EventSystem = FindObjectOfType<UnityEngine.EventSystems.EventSystem>();
            gra = FindObjectOfType<GraphicRaycaster>();
            rectTransform = GetComponent<RectTransform>();
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
            vector = this.transform.position;
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
            IsCardBeDrag = false;

            if (this.TryToDoInClient.Invoke()) {
                
            } else{
                this.rectTransform.position = vector;
            }
        }

        public GameObject GetTarget() {
            var list = GraphicRaycaster(Input.mousePosition);

            foreach (var goGraph in list) {
                //检测是否再目标上
                if (goGraph.gameObject.tag.Equals("target")) {
                    // todo 返回一个值
                    return goGraph.gameObject;
                }
            }

            return null;
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
}