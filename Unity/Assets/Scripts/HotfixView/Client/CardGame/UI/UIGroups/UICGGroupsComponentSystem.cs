using UnityEngine;

namespace ET.Client {
    [EntitySystemOf(typeof(UICGGroupsComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGGroupsComponent))]
    public static partial class UICGGroupsComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UICGGroupsComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.NewGroupButton = rc.Get<GameObject>("NewGroupButton");
            self.Group = rc.Get<GameObject>("Group");
            self.Next = rc.Get<GameObject>("Next");
            self.Last = rc.Get<GameObject>("Last");
            self.Groups = rc.Get<GameObject>("Groups");
            self.Back = rc.Get<GameObject>("Back");
        }
    }
}