using UnityEngine;

namespace ET.Client {
    
    [UIEvent(UIType.UICGGame)]
    public class UICGGameEvent : AUIEvent {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            string assetsName = $"Assets/Bundles/UI/CardGame/{UIType.UICGGame}.prefab";
            GameObject bundleGameObject = await uiComponent.Room().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
            GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, uiComponent.UIGlobalComponent.GetLayer((int)uiLayer));
            UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UICGGame, gameObject);
            UICGGameComponent uicgGameComponent = ui.AddComponent<UICGGameComponent>();
            uicgGameComponent.AddComponent<UIAnimComponent>();
            UICGGameHelper.CreateSelectInfo(uicgGameComponent);
            await uicgGameComponent.CreateUIShowCard();
            await uicgGameComponent.CreateUIUnitShowInfo();
            await uicgGameComponent.CreateHeroAndAgent();
            return ui;
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}