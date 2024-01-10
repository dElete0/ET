using DG.Tweening;
using UnityEngine;

namespace ET.Client {
    [EntitySystemOf(typeof(UIAnimComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIAnimComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UIAnimComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UIAnimComponent self)
        {
            self.Sequence = DOTween.Sequence();
        }

        public static Sequence GetSequence(this ET.Client.UIAnimComponent self)
        {
            if (!self.Sequence.active)
                self.Sequence = DOTween.Sequence();
            return self.Sequence;
        }

        public static UIUnitInfo GetUnitInfoByGo(this UIAnimComponent self, GameObject go)
        {
            foreach (UIUnitInfo info in self.Children.Values)
            {
                if (info.CardGo.Equals(go))
                    return info;
            }

            return null;
        }
    }
}