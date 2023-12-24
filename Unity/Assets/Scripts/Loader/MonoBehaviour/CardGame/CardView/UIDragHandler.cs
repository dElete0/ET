using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace ET.Client {
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIDragHandler : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler {
        // private RectTransform rectTrans;
        // private CanvasGroup canvasGroup;

        
        private HandlerState curState;
        
        public Action<PointerEventData> onBeginDrag;
        public Action<PointerEventData> onDrag;
        public Action<PointerEventData> onEndDrag;

        public PointerEventData eventData;

        // private void Start()
        // {
        //     rectTrans = GetComponent<RectTransform>();
        //     canvasGroup = GetComponent<CanvasGroup>();
        // }
        public void OnBeginDrag(PointerEventData eventData)
        {
            // 开始拖拽时，禁用射线检测和设置透明度
            Log.Debug("OnBeginDrag");
            // canvasGroup.blocksRaycasts = false;
            // canvasGroup.alpha = 0.35f;
            this.curState = HandlerState.BeginDrg;
            this.eventData = eventData;
            this.onBeginDrag?.Invoke(eventData);
        }
        public void OnDrag(PointerEventData eventData)
        {
            // 拖拽时，更新位置
            Log.Debug("OnDrag");
            //rectTrans.anchoredPosition += eventData.delta;
            //transform.position Input.mousePosition;//0PTIONAL
            this.curState = HandlerState.OnDrag;
            this.eventData = eventData;
            this.onDrag?.Invoke(eventData);
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            // 结束拖拽时，启用射线检测和恢复透明度
            Log.Debug("OnEndDrag");
            // canvasGroup.blocksRaycasts = true;
            // canvasGroup.alpha = 1f;
            this.curState = HandlerState.EndDrag;
            this.eventData = eventData;
            this.onEndDrag?.Invoke(eventData);
        }
    }
    
    
    public enum HandlerState
    {
        None,
        BeginDrg,
        OnDrag,
        EndDrag,
    }
}