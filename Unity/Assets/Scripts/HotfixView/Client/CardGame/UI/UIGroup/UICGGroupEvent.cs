using UnityEngine;

namespace ET.Client {
    
    [UIEvent(UIType.UICGGroup)]
    public class UICGGroupEvent : AUIEvent {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            string assetsName = $"Assets/Bundles/UI/CardGame/{UIType.UICGGroup}.prefab";
            GameObject bundleGameObject = await uiComponent.Room().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, uiComponent.UIGlobalComponent.GetLayer((int)uiLayer));
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UICGGroup, gameObject);
            ui.AddComponent<UICGGroupComponent>();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}