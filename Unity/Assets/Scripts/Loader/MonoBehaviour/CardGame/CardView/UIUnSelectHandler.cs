using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET {
    public class UIUnSelectHandler : MonoBehaviour, IPointerClickHandler {
        public Action Canel;

        public void OnPointerClick(PointerEventData eventData) {
            this.Canel.Invoke();
        }
    }
}