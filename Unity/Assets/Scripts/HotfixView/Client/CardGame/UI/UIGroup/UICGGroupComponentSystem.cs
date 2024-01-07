using UnityEngine;

namespace ET.Client {
    [EntitySystemOf(typeof(UICGGroupComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGGroupComponent))]
    public static partial class UICGGroupComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UICGGroupComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.BuildOver = rc.Get<GameObject>("BuildOver");
            self.UIGroupAgent1 = rc.Get<GameObject>("UIGroupAgent1");
            self.UIGroupAgent2 = rc.Get<GameObject>("UIGroupAgent2");
            self.UIGroupHero = rc.Get<GameObject>("UIGroupHero");
            self.Layout = rc.Get<GameObject>("Layout");
            self.UICard1 = rc.Get<GameObject>("UICard1");
            self.UICard2 = rc.Get<GameObject>("UICard2");
            self.UICard3 = rc.Get<GameObject>("UICard3");
            self.UICard4 = rc.Get<GameObject>("UICard4");
            self.UICard5 = rc.Get<GameObject>("UICard5");
            self.UICard6 = rc.Get<GameObject>("UICard6");
            self.UICard7 = rc.Get<GameObject>("UICard7");
            self.UICard8 = rc.Get<GameObject>("UICard8");
            self.UICard9 = rc.Get<GameObject>("UICard9");
            self.UICard10 = rc.Get<GameObject>("UICard10");
            self.Last = rc.Get<GameObject>("Last");
            self.Next = rc.Get<GameObject>("Next");
            self.Grey = rc.Get<GameObject>("Grey");
            self.Black = rc.Get<GameObject>("Black");
            self.Green = rc.Get<GameObject>("Green");
            self.White = rc.Get<GameObject>("White");
            self.Blue = rc.Get<GameObject>("Blue");
            self.Red = rc.Get<GameObject>("Red");
            self.Agent = rc.Get<GameObject>("Agent");
            self.Hero = rc.Get<GameObject>("Hero");
        }
    }
}