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
            if (self.TargetPos != self.CardGo.transform.position)
            {
                self.IsMove = true;
                DOTween.Sequence()
                        .Append(self.CardGo.transform.DOMove(self.TargetPos, 0.2f))
                        .AppendCallback(() => { self.IsMove = false; });
            }
        }

        public static void AttackTo(this UIUnitInfo self, Vector3 vector) {
            Vector3 backTarget = 0.15f * (self.CardGo.transform.position - vector) + self.CardGo.transform.position;
            self.GetParent<UIAnimComponent>().GetSequence()
                    .AppendCallback(() => self.IsMove = true)
                    .Append(self.CardGo.transform.DOMove(backTarget, 0.15f))
                    .Append(self.CardGo.transform.DOMove(vector, 0.3f))
                    .Append(self.CardGo.transform.DOMove(self.TargetPos, 0.13f))
                    .AppendCallback(() => self.IsMove = false);
        }

        public static void AppendCallback(this UIUnitInfo self, Action a) {
            self.GetParent<UIAnimComponent>().GetSequence().AppendCallback(a.Invoke);
        }
    }
}