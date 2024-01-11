using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

namespace ET.Client
{
    [ComponentOf(typeof(UICGGameComponent))]
    public class UIAnimComponent: Entity, IAwake, IUpdate {
        public bool IsLastSequenceOver = true;
        // 动画执行队列
        public Sequence Sequence;
        //等待执行的动作
        public List<Action<Sequence>> Actions = new List<Action<Sequence>>();
    }
}