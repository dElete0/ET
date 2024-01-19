using System;
using DG.Tweening;
using UnityEngine;

namespace ET.Client {
    [EntitySystemOf(typeof(UIAnimComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIAnimComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UIAnimComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UIAnimComponent self) {
        }
        [EntitySystem]
        private static void Update(this ET.Client.UIAnimComponent self)
        {
            if (self.IsLastSequenceOver && self.Actions.Count > 0) {
                self.IsLastSequenceOver = false;
                self.Sequence = DOTween.Sequence();
                foreach (var action in self.Actions) {
                    action.Invoke(self.Sequence);
                }
                self.Actions.Clear();

                self.Sequence.AppendCallback(() => self.IsLastSequenceOver = true);
            }
        }

        public static void AppendCallback(this ET.Client.UIAnimComponent self, TweenCallback tweenCallback)
        {
            self.Actions.Add((sequence) => sequence.AppendCallback(tweenCallback));
        }
        public static void Append(this ET.Client.UIAnimComponent self, Tween tween)
        {
            self.Actions.Add((sequence) => sequence.Append(tween));
        }

        public static void AppendInterval(this UIAnimComponent self, float time) {
            self.Actions.Add((sequence) => sequence.AppendInterval(time));
        }

        public static UIUnitInfo GetUnitInfoByGo(this UIAnimComponent self, GameObject go)
        {
            foreach (UIUnitInfo info in self.Children.Values)
            {
                if (info.CardGo.Equals(go)) {
                    return info;
                }
            }

            return null;
        }
    }
}