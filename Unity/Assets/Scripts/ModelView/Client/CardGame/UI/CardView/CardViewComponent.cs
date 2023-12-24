using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(GameCard))]
    public class CardViewComponent: Entity, IAwake, IUpdate
    {
        public RectTransform rectTrans;
        public CanvasGroup canvasGroup;
        public UIDragHandler DragHandler;
        public GameObject cardObject;

        // public EventTrigger EventTrigger;
        // public RectTransform rectTrans;
        // public CanvasGroup canvasGroup;
        public UnityEngine.UI.Text nameText;
        public UnityEngine.UI.Text descriptionText;
    }
}