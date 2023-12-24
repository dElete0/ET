namespace ET.Client
{
    //游戏中玩家行为处理
    [EntitySystemOf(typeof(PlayerGameActionComponent))]
    [FriendOf(typeof(PlayerGameActionComponent))]
    public static partial class GamePlayerActionComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.PlayerGameActionComponent self)
        {
        }
        [EntitySystem]
        private static void Destroy(this ET.Client.PlayerGameActionComponent self)
        {

        }
    }
}