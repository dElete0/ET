using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UICGLoginComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGLoginComponent))]
    public static partial class UICGLoginComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UICGLoginComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.loginBtn = rc.Get<GameObject>("LoginBtn");

            self.loginBtn.GetComponent<Button>().onClick.AddListener(() => { self.OnLogin(); });
            self.account = rc.Get<GameObject>("Account");
            self.password = rc.Get<GameObject>("Password");
        }

        public static void OnLogin(this UICGLoginComponent self)
        {
            LoginHelper.Login(
                self.Root(),
                self.account.GetComponent<InputField>().text,
                self.password.GetComponent<InputField>().text).Coroutine();
        }
    }
}