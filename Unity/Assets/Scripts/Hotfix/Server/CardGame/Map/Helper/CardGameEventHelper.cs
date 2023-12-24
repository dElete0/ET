using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server {
    public static partial class CardGameEventHelper {
        public static void M2C_GetHandCardsFromGroup(this Unit unit, int error, CardInfo info)
        {
            Log.Warning(unit.InstanceId.ToString());
            Log.Warning((unit.IScene == null).ToString());
            Log.Warning(unit.IScene.Fiber.ToString());
            Log.Warning(unit.IScene.Fiber.Root.ToString());
            Log.Warning(unit.Root().ToString());
            Log.Warning(unit.Root().GetComponent<MessageLocationSenderComponent>().ToString());
            Log.Warning(unit.Root().GetComponent<MessageLocationSenderComponent>().Get(LocationType.GateSession).ToString());
            
            MapMessageHelper.SendToClient(unit, new M2C_GetHandCardsFromGroup()
            {
                Error = error,
                Id = unit.Id, 
                Card = info,
            });
        }
    }
}