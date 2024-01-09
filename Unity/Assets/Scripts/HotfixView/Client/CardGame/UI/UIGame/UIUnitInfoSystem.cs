using DG.Tweening;

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
    }
}