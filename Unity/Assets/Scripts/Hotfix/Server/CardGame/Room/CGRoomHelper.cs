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
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player1, 3));
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player2, 4));
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.TurnStart(roomEventTypeComponent, player1));
            }
            else
            {
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player1, 4));
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.GetHandCardsFromGroup(roomEventTypeComponent, player2, 3));
                roomEventTypeComponent.BroadAndSettleEvent(GameEventFactory.TurnStart(roomEventTypeComponent, player2));
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
            {
                RoomCard card = RoomCardFactory.CreateUnitCard(cards, 300008);
                playerInfo.Groups.Add(card.Id);
            }
            for (int i = 0; i < 10; i++)
            {
                RoomCard card = RoomCardFactory.CreateUnitCard(cards, 3000001);
                playerInfo.Groups.Add(card.Id);
            }

            for (int i = 0; i < 10; i++) {
                RoomCard card = RoomCardFactory.CreateMagic(cards, 5000015);
                playerInfo.Groups.Add(card.Id);
            }
            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_MyGroupCount() { Count = playerInfo.Groups.Count });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_EnemyGroupCount() { Count = playerInfo.Groups.Count });

            //英雄
            RoomCard hero = RoomCardFactory.CreateHero(cards, 10001);
            playerInfo.Hero = hero.Id;
            RoomMessageHelper.ServerSendMessageToClient(player, new Room2C_NewHero() { Hero = hero.RoomCard2HeroInfo() });
            RoomMessageHelper.BroadCastWithOutPlayer(player, new Room2C_EnemyNewHero() { Hero = hero.RoomCard2HeroInfo() });

            //干员
            RoomCard agent1 = RoomCardFactory.CreateAgent(cards, 200001);
            RoomCard agent2 = RoomCardFactory.CreateAgent(cards, 200002);
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
                CardPowers = powers,
            };
        }
    }
}
