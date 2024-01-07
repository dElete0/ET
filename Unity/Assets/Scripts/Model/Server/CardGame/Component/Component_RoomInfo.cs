namespace ET.Server {
    [ComponentOf(typeof(GameRoom))]
    public partial class Component_RoomInfo : Entity, IAwake {
        public EntityRef<Player> player1, player2;
    }
}