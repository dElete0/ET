namespace ET.Server {
    [ComponentOf(typeof(GamePlayer))]
    public partial class ServerComponent_Player : Entity, IAwake {
        public Player player;
    }
}