using System;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.AOIEntity))]
    public static partial class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                    {
                        Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);

                        unitComponent.Add(unit);
                        return unit;
                    }
                case UnitType.Monster:
                    {
                        Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);

                        unitComponent.Add(unit);
                        return unit;
                    }
                case UnitType.NPC:
                    {
                        Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);

                        unitComponent.Add(unit);
                        // 加入aoi
                        unit.AddComponent<AOIEntity, int, float3>(9 * 1000, unit.Position);
                        return unit;
                    }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
    }
}