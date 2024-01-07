using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.CardGame)]
    public class CGSceneChangeStart_AddComponent: AEvent<Scene, CGSceneChangeStart>
    {
        protected override async ETTask Run(Scene clientScene, CGSceneChangeStart args)
        {
            Room room = clientScene.GetComponent<Room>();
            ResourcesLoaderComponent resourcesLoaderComponent = room.AddComponent<ResourcesLoaderComponent>();
            room.AddComponent<UIComponent>();
            // 创建loading界面
            
            
            // 创建房间UI
            await UIHelper.Remove(clientScene, UIType.UICGLobby);
            await UIHelper.Create(room, UIType.UICGGame, UILayer.Low);
            
            // 通知服务器，用户进来了
            
            // 加载场景资源
            // await resourcesLoaderComponent.LoadSceneAsync($"Assets/Bundles/Scenes/{room.Name}.unity", LoadSceneMode.Single);
        }
    }
}