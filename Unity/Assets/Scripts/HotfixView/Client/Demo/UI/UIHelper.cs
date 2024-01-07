namespace ET.Client
{
    public static class UIHelper
    {
        [EnableAccessEntiyChild]
        public static async ETTask<UI> Create(Entity scene, string uiType, UILayer uiLayer)
        {
            UI ui = await scene.GetComponent<UIComponent>().Create(uiType, uiLayer);
            ui.GameObject.SetActive(true);
            return ui;
        }
        
        [EnableAccessEntiyChild]
        public static async ETTask Remove(Entity scene, string uiType)
        {
            scene.GetComponent<UIComponent>().Remove(uiType);
            await ETTask.CompletedTask;
        }
        
        [EnableAccessEntiyChild]
        public static async ETTask<UI> Get(Entity scene, string uiType)
        {
            UI ui = scene.GetComponent<UIComponent>().Get(uiType);
            await ETTask.CompletedTask;
            return ui;
        }
    }
}