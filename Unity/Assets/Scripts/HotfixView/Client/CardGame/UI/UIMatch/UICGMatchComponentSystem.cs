using UnityEngine;
using UnityEngine.UI;

namespace ET.Client {
    [EntitySystemOf(typeof(UIMatchComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIMatchComponent))]
    public static partial class UICGMatchComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.UIMatchComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.NewGroupButton = rc.Get<GameObject>("NewGroupButton");
            self.Group = rc.Get<GameObject>("Group");
            self.Next = rc.Get<GameObject>("Next");
            self.Last = rc.Get<GameObject>("Last");
            self.Groups = rc.Get<GameObject>("Groups");
            self.Back = rc.Get<GameObject>("Back");
            self.Match = rc.Get<GameObject>("Match");
            
            self.Match.GetComponent<Button>().onClick.AddListener(() => {
                EnterMapHelper.GameMatchWithAi(self.Fiber()).Coroutine();
            });
            self.Next.GetComponent<Button>().onClick.AddListener(() => {
                Next(self);
            });
        }

        private static void Next(UIMatchComponent ui) {
            
        }
    }
}