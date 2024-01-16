using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ET {
    public class UISelectHandler : MonoBehaviour, IPointerClickHandler {
        public Action ToDo;

        public void OnPointerClick(PointerEventData eventData) {
            this.ToDo.Invoke();
        }
    }
}