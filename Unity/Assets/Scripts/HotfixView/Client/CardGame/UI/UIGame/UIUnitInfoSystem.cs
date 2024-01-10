using System;
using DG.Tweening;
using UnityEngine;

namespace ET.Client {
    [EntitySystemOf(typeof(UIUnitInfo))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UIUnitInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UIUnitInfo self, UnityEngine.GameObject args2)
        {
            self.CardGo = args2;
            self.TargetPos = args2.transform.position;
        }
        [EntitySystem]
        private static void Update(this ET.Client.UIUnitInfo self)
        {
            if (self.IsMove) return;
            if (self.IsDrag) return;
            if (self.IsScaleChange) return;
            bool isPos = self.TargetPos != self.CardGo.transform.position;
            bool isScale = self.TargetScale != self.CardGo.transform.localScale.x;
            if (isPos) {
                Sequence sequence = DOTween.Sequence();
                self.IsMove = true;
                sequence.Append(self.CardGo.transform.DOMove(self.TargetPos, 0.2f));
                sequence.AppendCallback(() => { self.IsMove = false; });
            }
            if (isScale) {
                Sequence sequence = DOTween.Sequence();
                self.IsScaleChange = true;
                sequence.Append(self.CardGo.transform.DOScale(self.TargetScale * Vector3.one, 0.2f));
                sequence.AppendCallback(() => { self.IsScaleChange = false; });
            }
        }

        public static void AttackTo(this UIUnitInfo self, UIUnitInfo target) {
            var targetPos = target.CardGo.transform.position;
            var pos = self.CardGo.transform.position;
            Vector3 backTarget = 0.15f * (pos - targetPos) + pos;
            Sequence sequence = self.GetParent<UIAnimComponent>().GetSequence();
            sequence.AppendCallback(() => self.IsMove = true);
            sequence.Append(self.CardGo.transform.DOMove(backTarget, 0.15f));
            sequence.Append(self.CardGo.transform.DOMove(targetPos, 0.3f));
            sequence.AppendCallback(() => {
                        target.CardGo.transform.DOShakeRotation(1f, new Vector3(10, 10, 10), 10, 180);
                    });
            sequence.Append(self.CardGo.transform.DOMove(self.TargetPos, 0.13f));
            sequence.AppendCallback(() => self.IsMove = false);
        }

        public static void AppendCallback(this UIUnitInfo self, Action a) {
            self.GetParent<UIAnimComponent>().GetSequence().AppendCallback(a.Invoke);
        }
    }
}