namespace ET.Server; 

public static class TriggerEventFactory {
    public static TriggerEvent MyTurnStart(RoomPlayer roomPlayer) {
        TriggerEvent turnStart = new((gameEvent) => {
            if (gameEvent.GameEventType == GameEventType.TurnStart &&
                gameEvent.Id1 == roomPlayer.Id) {
                return true;
            }
            return false;
        });
        return turnStart;
    } 
    
    public static TriggerEvent TurnStart() {
        TriggerEvent turnStart = new((gameEvent) => {
            if (gameEvent.GameEventType == GameEventType.TurnStart) {
                return true;
            }
            return false;
        });
        return turnStart;
    } 
}