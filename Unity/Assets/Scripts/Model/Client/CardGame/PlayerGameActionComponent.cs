namespace ET.Client
{
    [ComponentOf(typeof(GameRoom))]
    public class PlayerGameActionComponent: Entity, IAwake, IDestroy
    {
        public int fiberId;

        public ActorId netClientActorId;
    }
}