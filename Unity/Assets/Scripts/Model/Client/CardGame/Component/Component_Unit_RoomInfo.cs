namespace ET.Client {

    [ComponentOf(typeof(Unit))]
    public class Component_Unit_RoomInfo : Entity, IAwake {
        public EntityRef<GameRoom> Room;
        public GamePlayer Player;
    }
}