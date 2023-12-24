using System;
using UnityEngine;

namespace ET.Client {
    [UIEvent(UIType.UIGame)]
    public class UIGameEvent : AUIEvent {
        public override async ETTask<UI> OnCreate(UIComponent uiComponent, UILayer uiLayer)
        {
            try
            {
                string assetsName = $"Assets/Bundles/UI/Demo/{UIType.UIGame}.prefab";
                GameObject bundleGameObject = await uiComponent.Scene().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(assetsName);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, uiComponent.UIGlobalComponent.GetLayer((int)uiLayer));
                UI ui = uiComponent.AddChild<UI, string, GameObject>(UIType.UIGame, gameObject);

                ui.AddComponent<UIHelpComponent>();
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }

        public override void OnRemove(UIComponent uiComponent)
        {
        }
    }
}