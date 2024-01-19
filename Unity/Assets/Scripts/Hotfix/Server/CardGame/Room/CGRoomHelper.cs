using System;
using System.Collections.Generic;

namespace ET.Server {
    [FriendOfAttribute(typeof(ET.Server.CGServerUpdater))]
    [FriendOfAttribute(typeof(ET.Server.CardGameComponent_Player))]
    [FriendOfAttribute(typeof(ET.RoomCard))]
    [FriendOfAttribute(typeof(ET.RoomPlayer))]
    public static class CGRoomHelper
    {
        public static async ETTask RoomCardInit(Room room)
        {
            RoomEventTypeComponent roomEventTypeComponent = room.GetComponent<RoomEventTypeComponent>();
            //对局先后手
            int a = RandomGenerator.RandomNumber(0, 2);
            //初始化双方卡组等
            RoomPlayer player1 = null, player2 = null;
            bool isplayer1 = true;
            foreach (RoomPlayer roomPlayer in room.GetComponent<RoomServerComponent>().Children.Values)
            {
                if (isplayer1)
                {
                    player1 = roomPlayer;
                    isplayer1 = false;
                }
                else
                {
                    player2 = roomPlayer;
                }
                SetGroupDemo(roomPlayer);
            }

            //先后手
            room.GetComponent<CGServerUpdater>().NowPlayer = (a == 0) ? player1.Id : player2.Id;

            // Log.Warning(room.SceneType.ToString());
            // 抽卡
            if (room.GetComponent<CGServerUpdater>().NowPlayer == player1.Id)
            {
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player1, 3), new EventInfo(0));
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player2, 4), new EventInfo(0));
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.TurnStart(roomEventTypeComponent, player1), new EventInfo(0));
            }
            else
            {
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player1, 4), new EventInfo(0));
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player2, 3), new EventInfo(0));
                await roomEventTypeComponent.SettleEventWithLock(GameEventFactory.TurnStart(roomEventTypeComponent, player2), new EventInfo(0));
            }

            room.GetComponent<CGServerUpdater>().GameState = GameState.Run;
            

            //玩家开始回合
            await ETTask.CompletedTask;
        }

        //玩家初始化
        private static void SetGroupDemo(RoomPlayer player)
        {
            //牌库
            CardGameComponent_Cards cards = player.GetParent<Room>().GetComponent<CardGameComponent_Cards>();
            CardGameComponent_Player playerInfo = player.GetComponent<CardGameComponent_Player>();
            List<int> baseIds = new List<int>() {
                300008, 
                3000010,
                5000062,
                5000063,
                5000032,
                5000064,
                5000018,
                5000003,
                5000032,
                3000006,
                3000011,
                5000060,
                3000012,
                5000059,
                3000010,
                3000010,
                3000001,
                3000001,
                3000010,
                3000010,
                3000010,
                4000001,
                4000001,
                5000015,
                3000001,
                5000015,
                5000015,
                5000015,
                5000015,
            };
            foreach (var id in baseIds) {
                RoomCard card = RoomCardFactory.CreateGroupCard(cards, id, player.Id);
                playerInfo.Groups.Add(card.Id);
            }
            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_MyGroupCount() { Count = playerInfo.Groups.Count });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_EnemyGroupCount() { Count = playerInfo.Groups.Count });

            //英雄
            RoomCard hero = RoomCardFactory.CreateHero(cards, 10001, player.Id);
            playerInfo.Hero = hero.Id;
            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_NewHero() { Hero = hero.RoomCard2HeroInfo() });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_EnemyNewHero() { Hero = hero.RoomCard2HeroInfo() });

            //干员
            RoomCard agent1 = RoomCardFactory.CreateAgent(cards, 200001, player.Id);
            RoomCard agent2 = RoomCardFactory.CreateAgent(cards, 200002, player.Id);
            playerInfo.Agent1 = agent1.Id;
            playerInfo.Agent2 = agent2.Id;
            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_NewAgent() { Agent1 = agent1.RoomCard2AgentInfo(), Agent2 = agent2.RoomCard2AgentInfo() });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_EnemyNewAgent() { Agent1 = agent1.RoomCard2AgentInfo(), Agent2 = agent2.RoomCard2AgentInfo() });
        }

        public static RoomPlayer GetEnemy(this RoomPlayer player)
        {
            RoomServerComponent roomServerComponent = player.GetParent<Room>().GetComponent<RoomServerComponent>();

            MessageLocationSenderComponent messageLocationSenderComponent = player.GetParent<Room>().Root().GetComponent<MessageLocationSenderComponent>();
            foreach (var kv in roomServerComponent.Children)
            {
                RoomPlayer roomPlayer = kv.Value as RoomPlayer;

                if (player == roomPlayer || !roomPlayer.IsOnline)
                {
                    continue;
                }

                return roomPlayer;
            }

            return null;
        }

        public static RoomCardInfo RoomCard2MyHandCardInfo(this RoomCard roomCard)
        {
            return new RoomCardInfo()
            {
                CardId = roomCard.Id,
                Type = (int)roomCard.CardType,
                BaseId = roomCard.ConfigId,
                Attack = roomCard.Attack,
                HP = roomCard.HP,
                Cost = roomCard.Cost,
                UseCardType = (int)roomCard.UseCardType,
                CardType = (int)roomCard.CardType,
                Red = roomCard.Red,
                Blue = roomCard.Blue,
                Green = roomCard.Green,
                Grey = roomCard.Grey,
                White = roomCard.White,
                Black = roomCard.Black,
            };
        }

        public static RoomCardInfo RoomCard2EnemyHandCardInfo(this RoomCard roomCard)
        {
            return new RoomCardInfo() { CardId = roomCard.Id, };
        }

        public static RoomCardInfo RoomCard2HeroInfo(this RoomCard roomCard)
        {
            return new RoomCardInfo()
            {
                CardId = roomCard.Id,
                Type = (int)roomCard.CardType,
                BaseId = roomCard.ConfigId,
                Attack = roomCard.Attack,
                HP = roomCard.HP,
                Cost = roomCard.Cost,
                Red = roomCard.Red,
                Blue = roomCard.Blue,
                Green = roomCard.Green,
                Grey = roomCard.Grey,
                White = roomCard.White,
                Black = roomCard.Black,
            };
        }

        public static RoomCardInfo RoomCard2AgentInfo(this RoomCard roomCard)
        {
            return new RoomCardInfo()
            {
                CardId = roomCard.Id,
                Type = (int)roomCard.CardType,
                BaseId = roomCard.ConfigId,
                Attack = roomCard.Attack,
                HP = roomCard.HP,
                Cost = roomCard.Cost,
                Red = roomCard.Red,
                Blue = roomCard.Blue,
                Green = roomCard.Green,
                Grey = roomCard.Grey,
                White = roomCard.White,
                Black = roomCard.Black,
            };
        }

        public static List<RoomCardInfo> RoomCardList2UnitInfoList(this CardGameComponent_Player playerCards, CardGameComponent_Cards cards) {
            List<RoomCardInfo> cardInfos = new List<RoomCardInfo>();
            foreach (var unitId in playerCards.Units)
            {
                RoomCard unit = cards.GetChild<RoomCard>(unitId);
                cardInfos.Add(unit.RoomCard2AgentInfo());
            }
            return cardInfos;
        }

        public static RoomCardInfo RoomCard2UnitInfo(this RoomCard roomCard) {
            List<int> powers = new List<int>();
            foreach (Power_Type type in roomCard.AttributePowers) {
                powers.Add((int)type);
            }
            return new RoomCardInfo()
            {
                CardId = roomCard.Id,
                Type = (int)roomCard.CardType,
                BaseId = roomCard.ConfigId,
                Attack = roomCard.Attack,
                HP = roomCard.HP,
                Cost = roomCard.Cost,
                Armor = roomCard.Armor,
                CardPowers = powers,
            };
        }
        
        //获得随机角色
        11
        
        //获得随机友方单位
        11
        
        //获得随机敌方单位
        11
        
        //获得随机单位
        11
        
        //获得随机干员
        11
        //获得随机友方干员
        11
        //获得随机敌方干员
        11
        //获得随机友方角色
        11
        //获得随机敌方角色
    }
}
