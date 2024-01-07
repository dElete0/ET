using UnityEngine;

namespace ET.Client {
    
    [UIEvent(UIType.UICGGroups)]
    public class UICGGroupsEvent : AUIEvent {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            string assetsName = $"Assets/Bundles/UI/CardGame/{UIType.UICGGroups}.prefab";
            GameObject bundleGameObject = await uiComponent.Room().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, uiComponent.UIGlobalComponent.GetLayer((int)uiLayer));
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UICGGroups, gameObject);
            ui.AddComponent<UICGGroupsComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}