namespace ET.Server {
    [ComponentOf(typeof(GameRoom))]
    public partial class Component_RoomInfo : Entity, IAwake {
        public Player player1, player2;
    }
}