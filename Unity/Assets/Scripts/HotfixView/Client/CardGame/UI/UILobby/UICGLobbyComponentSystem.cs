using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UICGLobbyComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGLobbyComponent))]
    public static partial class UICGLobbyComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UICGLobbyComponent self)
        {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            self.enterMap = rc.Get<GameObject>("EnterMap");
            self.enterMap.GetComponent<Button>().onClick.AddListener(() =>
            {
                self.EnterMap().Coroutine();
            });
            
            self.replay = rc.Get<GameObject>("Replay").GetComponent<Button>();
            self.replayPath = rc.Get<GameObject>("ReplayPath").GetComponent<InputField>();
            self.replay.onClick.AddListener(self.Replay);
        }

        private static async ETTask EnterMap(this UICGLobbyComponent self)
        {
            await EnterMapHelper.GameMatchWithAi(self.Fiber());
        }
        
        private static void Replay(this UICGLobbyComponent self)
        {
            byte[] bytes = File.ReadAllBytes(self.replayPath.text);
            
            Replay replay = MemoryPackHelper.Deserialize(typeof (Replay), bytes, 0, bytes.Length) as Replay;
            Log.Debug($"start replay: {replay.Snapshots.Count} {replay.FrameInputs.Count} {replay.UnitInfos.Count}");
            LSSceneChangeHelper.SceneChangeToReplay(self.Root(), replay).Coroutine();
        }
    }
}