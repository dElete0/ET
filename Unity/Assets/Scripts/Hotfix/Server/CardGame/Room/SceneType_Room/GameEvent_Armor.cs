
namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.Server.CardEventTypeComponent))]
    public static class GameEvent_Armor
    {
        public static async ETTask ToDo_GetArmor(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor, int num)
        {
            RoomPlayer player = actor.GetOwner();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            RoomCard hero = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>().GetChild<RoomCard>(playerInfo.Hero);
            hero.Armor += num;

            Room2C_GetArmor MyArmor = new Room2C_GetArmor() { Num = num, IsMy = true, Now = hero.Armor };
            Room2C_GetArmor enemyArmor = new Room2C_GetArmor() { Num = num, IsMy = false, Now = hero.Armor };
            RoomMessageHelper.ServerSendMessageToClient(player, MyArmor);
            RoomMessageHelper.BroadCastWithOutPlayer(player, enemyArmor);
        }

        public static async ETTask ToDo_TargetGetPower(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor, RoomCard target, Power_Type powerType)
        {
            if (!target.AttributePowers.ContainsKey(powerType))
            {
                target.AttributePowers.Add(powerType, 1);
            }
            else
            {
                target.AttributePowers[powerType]++;
            }

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = target.RoomCard2UnitInfo() };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
        }
        
        public static async ETTask ToDo_TargetLosePower(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor, RoomCard target, Power_Type powerType, int num) {
            bool isFlash = false;
            if (!target.AttributePowers.ContainsKey(powerType)) {
                return;
            } else {
                if (num < 1) {
                    target.AttributePowers.Remove(powerType);
                    isFlash = true;
                } else {
                    target.AttributePowers[powerType] -= num;
                    if (target.AttributePowers[powerType] < 1) {
                        target.AttributePowers.Remove(powerType);
                        isFlash = true;
                    }
                }
            }

            if (isFlash) {
                Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = target.RoomCard2UnitInfo() };
                RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
            }
        }

        public static async ETTask ToDo_MyHeroGetTargetPowerThisTurn(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor, Power_Type type)
        {
            RoomCard hero = actor.GetOwner().GetHero();
            if (!hero.AttributePowers.ContainsKey(type))
            {
                hero.AttributePowers.Add(type, 1);
            } else {
                hero.AttributePowers[type]++;
            }

            hero.GetComponent<CardEventTypeComponent>().AllGameEventTypes.Add(TriggerEventFactory.TurnOver(), GameEventFactory.TargetLosePower(roomEventTypeComponent, actor, hero, type, 1));

            Room2C_FlashUnit message = new Room2C_FlashUnit() { Unit = hero.RoomCard2HeroInfo() };
            RoomMessageHelper.BroadCast(roomEventTypeComponent.GetParent<Room>(), message);
        }

        public static async ETTask ToDo_SwapArmor(this RoomEventTypeComponent roomEventTypeComponent, RoomCard actor)
        {
            RoomPlayer player = actor.GetOwner();
            RoomPlayer enemy = player.GetEnemy();
            CardGameComponent_Cards cards = roomEventTypeComponent.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            CardGameComponent_Player myInfo = player.GetComponent<CardGameComponent_Player>();
            CardGameComponent_Player enemyInfo = enemy.GetComponent<CardGameComponent_Player>();
            RoomCard myHero = cards.GetChild<RoomCard>(myInfo.Hero);
            RoomCard enemyHero = cards.GetChild<RoomCard>(enemyInfo.Hero);
            if (myHero.Armor == 0 && enemyHero.Armor == 0)
            {
                return;
            }

            int myNow = myHero.Armor;
            int enemyNow = enemyHero.Armor;
            if (myNow > 0)
            {
                myHero.Armor = 0;
                Room2C_LoseArmor myMessage = new Room2C_LoseArmor() { IsMy = true };
                Room2C_LoseArmor enemyMessage = new Room2C_LoseArmor() { IsMy = false };
                RoomMessageHelper.ServerSendMessageToClient(player, myMessage);
                RoomMessageHelper.BroadCastWithOutPlayer(player, enemyMessage);
                if (enemyNow > 0)
                {
                    myHero.Armor = enemyNow;
                    Room2C_GetArmor MyArmor = new Room2C_GetArmor() { Num = enemyNow, IsMy = true, Now = enemyNow };
                    Room2C_GetArmor enemyArmor = new Room2C_GetArmor() { Num = enemyNow, IsMy = false, Now = enemyNow };
                    RoomMessageHelper.ServerSendMessageToClient(player, MyArmor);
                    RoomMessageHelper.BroadCastWithOutPlayer(player, enemyArmor);
                }
            }

            if (enemyNow > 0)
            {
                enemyHero.Armor = 0;
                Room2C_LoseArmor myMessage = new Room2C_LoseArmor() { IsMy = true };
                Room2C_LoseArmor enemyMessage = new Room2C_LoseArmor() { IsMy = false };
                RoomMessageHelper.ServerSendMessageToClient(enemy, myMessage);
                RoomMessageHelper.BroadCastWithOutPlayer(enemy, enemyMessage);
                if (myNow > 0)
                {
                    enemyHero.Armor = myNow;
                    Room2C_GetArmor MyArmor = new Room2C_GetArmor() { Num = myNow, IsMy = true, Now = myNow };
                    Room2C_GetArmor enemyArmor = new Room2C_GetArmor() { Num = myNow, IsMy = false, Now = myNow };
                    RoomMessageHelper.ServerSendMessageToClient(enemy, MyArmor);
                    RoomMessageHelper.BroadCastWithOutPlayer(enemy, enemyArmor);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}