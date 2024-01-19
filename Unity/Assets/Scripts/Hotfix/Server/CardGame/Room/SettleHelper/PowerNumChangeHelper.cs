using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class PowerNumChangeHelper
    {
        //获得法强
        public static int GetMagicAddByRoomPlayer(this RoomPlayer player)
        {
            int magicAdd = 0;
            CardGameComponent_Cards cards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            List<long> unitIds = player.GetComponent<CardGameComponent_Player>().Units;
            foreach (var unitId in unitIds)
            {
                RoomCard unit = cards.GetChild<RoomCard>(unitId);
                foreach (var power in unit.OtherPowers)
                {
                    if (power.PowerType == Power_Type.AddMagicDamage) {
                        magicAdd += power.Count1;
                    }
                }
            }

            return magicAdd;
        }

        public static int GetInheritByBaseId(this RoomPlayer player, int baseId) {
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            if (playerInfo.InheritCount.ContainsKey(baseId)) {
                return playerInfo.InheritCount[baseId];
            }

            return 0;
        }

        //受到法术增强影响的数值编号
        public static int GetCountNumByAddMagicDamage(Power_Type powerType)
        {
            switch (powerType)
            {
                case Power_Type.DamageHurt:
                case Power_Type.Desecrate:
                case Power_Type.DamageAllUnit:
                    return 1;
                default:
                    return 0;
            }
        }

        //受到传承影响的数值编号
        public static int GetCountNumByInherit(Power_Type powerType)
        {
            switch (powerType)
            {
                case Power_Type.TargetGetAttribute:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}

