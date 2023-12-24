namespace ET.Client {

    [ComponentOf(typeof(Unit))]
    public class Component_Unit_RoomInfo : Entity, IAwake {
        public GameRoom Room;
        public GamePlayer Player;
    }
}