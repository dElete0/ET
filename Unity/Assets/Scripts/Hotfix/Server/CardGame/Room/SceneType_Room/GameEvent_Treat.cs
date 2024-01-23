using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    public static class GameEvent_Treat
    {
        public static async ETTask ToDo_TreatTargets(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor, List<long> targetIds, int num) {
            Room room = roomEventTypeComponent.GetParent<Room>();
            CardGameComponent_Cards cards = room.GetComponent<CardGameComponent_Cards>();
            RoomPlayer player = actor.GetOwner();
            List<RoomCardInfo> roomCardInfos = new List<RoomCardInfo>();
            List<int> treatList = new List<int>();
            foreach (var targetId in targetIds) {
                RoomCard target = cards.GetChild<RoomCard>(targetId);
                int treat = 0;
                if (target.HP + num > target.HPMax) {
                    treat = target.HPMax - target.HP;
                    target.HP = target.HPMax;
                } else {
                    treat = num;
                    target.HP += num;
                }

                if (treat > 0) {
                    roomCardInfos.Add(target.RoomCard2UnitInfo());
                    treatList.Add(treat);
                }
            }

            Room2C_TreatTergets message = new Room2C_TreatTergets() { Cards = roomCardInfos, Nums = treatList };
            RoomMessageHelper.BroadCast(room, message);
            await ETTask.CompletedTask;
        }
    }
}