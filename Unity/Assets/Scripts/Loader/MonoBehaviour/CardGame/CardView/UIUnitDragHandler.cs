using System;
using System.Collections;

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ET {
    public class UIUnitDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
        //执行对目标释放的效果=>里头是发送消息to Server
        public Action<long> UseCardToServer;
        //仅Client内部调用
        public Func<Vector2, bool> TryToDoInClient;
        //拖拽的动态效果
        public Action<GameObject> DragShow;
        // CardId
        public long CardId;
        public int BaseId;
        public bool IsMy;

        public static List<UIUnitDragHandler> IsCardBeDrag = new List<UIUnitDragHandler>();

        public bool CanBeUsed = true;
        [FormerlySerializedAs("UseCardType")]
        public int UIUseCardType;
        [FormerlySerializedAs("CardType")]
        public int UICardType;
        //记录玩家开始拖拽时的位置
        private Vector3 vector;


        [Header("表示限制的区域")]
        public RectTransform LimitContainer;
        [Header("场景中Canvas")]
        public Canvas canvas;
        RectTransform rt;
        // 位置偏移量
        Vector3 offset = Vector3.zero;
        // 最小、最大X、Y坐标
        float minX, maxX, minY, maxY;

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

        public void OnMouseEnter() {
            this.IsMouseEnter = true;
        }

        public void OnMouseExit() {
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
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.enterEventCamera, out Vector3 globalMousePos)) {
                // 计算偏移量
                offset = rt.position - globalMousePos;
                // 设置拖拽范围
                SetDragRange();
            }
            vector = this.transform.position;
            IsCardBeDrag.Add(this);
        }

        /// <summary>
        /// 拖拽时持续调用
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData) {
            //if (!this.CanBeUsed) return;
            if (!IsMy) return;
            if (this.DragShow != null) this.DragShow.Invoke(null);
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos)) {
                this.GlobalMousePos = globalMousePos;
                rt.position = DragRangeLimit(globalMousePos + offset);
            }
        }
        

        /// <summary>
        /// 结束拖拽时执行一次
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData) {
            //判断是否拖拽到目标上
            if (!IsMy) return;
            Log.Warning("判断是否拖拽到目标上");
            GameObject target = null;
            bool isUsed;
            if (IsCardBeDrag.Contains(this)) IsCardBeDrag.Remove(this);

            if (this.TryToDoInClient.Invoke(this.GlobalMousePos)) {
                this.rectTransform.position = vector;
            } else{
                this.rectTransform.position = vector;
            }
        }

        public GameObject GetTarget(PointerEventData eventData) {
            /*string objectTag = eventData.pointerCurrentRaycast.gameObject.tag;
            Log.Warning(eventData.pointerCurrentRaycast.gameObject.ToString());
            Log.Warning("Raycast = " + objectTag);
            if (objectTag != null && objectTag.Equals("Target")) {
                GameObject targetItem = eventData.pointerCurrentRaycast.gameObject;
                return targetItem.transform.parent.gameObject;
            }*/
            
            GraphicRaycaster gr = canvas.GetComponent<GraphicRaycaster>();
            List<RaycastResult> results = new List<RaycastResult>();
            gr.Raycast(eventData, results);
            Log.Warning(results.Count);
            if (results.Count != 0)
            {
                foreach (var target in results) {
                    Log.Warning(target.gameObject.ToString());
                    Log.Warning(target.gameObject.transform.parent.gameObject.ToString());
                    Log.Warning(target.gameObject.tag);
                    if (target.gameObject.tag.Equals("Target")) {
                        // todo 返回一个值
                        return target.gameObject.transform.parent.gameObject;
                    }
                }
            }

            /*var list = GraphicRaycaster(eventData.position);
            Log.Warning(list.Count);
            foreach (var goGraph in list) {
                Log.Warning(goGraph.gameObject.ToString());
                //检测是否再目标上
                if (goGraph.gameObject.tag.Equals("Target")) {
                    // todo 返回一个值
                    return goGraph.gameObject.transform.parent.gameObject;
                }
            }*/

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

        // 设置最大、最小坐标
        void SetDragRange()
        {
            // 最小x坐标 = 容器当前x坐标 - 容器轴心距离左边界的距离 + UI轴心距离左边界的距离
            minX = LimitContainer.position.x
                    - LimitContainer.pivot.x * LimitContainer.rect.width * canvas.scaleFactor
                    + rt.rect.width * canvas.scaleFactor * rt.pivot.x;
            // 最大x坐标 = 容器当前x坐标 + 容器轴心距离右边界的距离 - UI轴心距离右边界的距离
            maxX = LimitContainer.position.x
                    + (1 - LimitContainer.pivot.x) * LimitContainer.rect.width * canvas.scaleFactor
                    - rt.rect.width * canvas.scaleFactor * (1 - rt.pivot.x);

            // 最小y坐标 = 容器当前y坐标 - 容器轴心距离底边的距离 + UI轴心距离底边的距离
            minY = LimitContainer.position.y
                    - LimitContainer.pivot.y * LimitContainer.rect.height * canvas.scaleFactor
                    + rt.rect.height * canvas.scaleFactor * rt.pivot.y;

            // 最大y坐标 = 容器当前x坐标 + 容器轴心距离顶边的距离 - UI轴心距离顶边的距离
            maxY = LimitContainer.position.y
                    + (1 - LimitContainer.pivot.y) * LimitContainer.rect.height * canvas.scaleFactor
                    - rt.rect.height * canvas.scaleFactor * (1 - rt.pivot.y);
        }
        // 限制坐标范围
        Vector3 DragRangeLimit(Vector3 pos)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            return pos;
        }
    }
}