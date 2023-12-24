namespace ET.Server {
    [EntitySystemOf(typeof(Component_Rooms))]
    [FriendOfAttribute(typeof(ET.Component_Rooms))]
    public static partial class ComponentSystem_Room
    {
        [EntitySystem]
        private static void Awake(this ET.Component_Rooms self)
        {

        }
    }
}