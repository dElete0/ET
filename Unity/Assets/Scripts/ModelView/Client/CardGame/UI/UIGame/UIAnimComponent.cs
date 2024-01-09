using DG.Tweening;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UICGGameComponent))]
    public class UIAnimComponent: Entity, IAwake {
        // 动画执行队列
        public Sequence Sequence;
    }
}