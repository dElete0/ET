namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class EnterRoom_CreateGameUICGGame : AEvent<Scene, EnterRoom>
    {
        protected override async ETTask Run(Scene scene, EnterRoom args)
        {
            Room room = scene.GetComponent<Room>();
            ResourcesLoaderComponent resourcesLoaderComponent = room.AddComponent<ResourcesLoaderComponent>();
            room.AddComponent<UIComponent>();
            
            // 创建loading界面
            await ETTask.CompletedTask;
        }
    }
}