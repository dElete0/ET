using DG.Tweening;

namespace ET.Client {
    [EntitySystemOf(typeof(UIAnimComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIAnimComponent))]
    public static partial class UIAnimComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UIAnimComponent self)
        {
            self.Sequence = DOTween.Sequence();
        }

        public static Sequence GetSequence(this ET.Client.UIAnimComponent self) {
            if (!self.Sequence.active)
                self.Sequence = DOTween.Sequence();
            return self.Sequence;
        }
    }
}