using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UIMainComponent))]
    [FriendOf(typeof(UIMainComponent))]
    public static partial class UIMainComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UIMainComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //匹配
            self.FindEnemy = rc.Get<GameObject>("FindEnemy");
            self.FindEnemy.GetComponent<Button>().onClick.AddListener(() => { self.FindEnemy().Coroutine(); });
            
            //人机
            self.FightWithAi = rc.Get<GameObject>("FightWithAi");
            self.FightWithAi.GetComponent<Button>().onClick.AddListener(() => { self.FightWithAi().Coroutine(); });
        }
        
        public static async ETTask FindEnemy(this UIMainComponent self)
        {
            Scene root = self.Root();
            await EnterMapHelper.EnterMapAsync(root);
            await UIHelper.Remove(root, UIType.UILobby);
        }
        
        public static async ETTask FightWithAi(this UIMainComponent self)
        {
            Scene root = self.Root();
            await EventSystem.Instance.PublishAsync(root, new FightWithAi());
        }
    }
}