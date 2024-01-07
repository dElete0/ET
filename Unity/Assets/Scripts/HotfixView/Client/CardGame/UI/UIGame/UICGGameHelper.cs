using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOfAttribute(typeof (ET.Client.UICGGameComponent))]
    public static partial class UICGGameHelper {

        public static void RemoveMyUnit(this UICGGameComponent ui, long CardId) {
            ui.RemoveUnit(CardId, true);
        }
        public static void RemoveEnemyUnit(this UICGGameComponent ui, long CardId) {
            ui.RemoveUnit(CardId, false);
        }

        private static void RemoveUnit(this UICGGameComponent ui, long CardId, bool IsMy) {
            var units = IsMy ? ui.MyFightUnits : ui.EnemyFightUnits;
            foreach (var unit in units) {
                if (unit.Key.CardId == CardId) {
                    unit.Value.CardGo.SetActive(false);
                    ui.UnitPool.Add(unit.Value.CardGo);
                    ui.MyFightUnits.Remove(unit.Key);
                    return;
                }
            }
        }

        public static async ETTask Room2C_GetHandCardsFromGroup(Room room, List<RoomCardInfo> cardInfos) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var cardInfo in cardInfos) {
                UIGetHandCardFromGroup(room, uicgGameComponent, cardInfo); 
            }
        }
        
        public static async ETTask Room2C_GetHandCardFromGroup(Room room, RoomCardInfo cardInfo) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>(); 
            UIGetHandCardFromGroup(room, uicgGameComponent, cardInfo);
        }

        public static async ETTask Room2C_GetColor(Room room, CardColor color, int num, bool isMy) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            if (isMy) {
                switch (color) {
                    case CardColor.Red:
                        uicgGameComponent.MyRed.text = num.ToString();
                        uicgGameComponent.MyRed.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Blue:
                        uicgGameComponent.MyBlue.text = num.ToString();
                        uicgGameComponent.MyBlue.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Black:
                        uicgGameComponent.MyBlack.text = num.ToString();
                        uicgGameComponent.MyBlack.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Green:
                        uicgGameComponent.MyGreen.text = num.ToString();
                        uicgGameComponent.MyGreen.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Grey:
                        uicgGameComponent.MyGrey.text = num.ToString();
                        uicgGameComponent.MyGrey.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.White:
                        uicgGameComponent.MyWhite.text = num.ToString();
                        uicgGameComponent.MyWhite.transform.parent.gameObject.SetActive(true);
                        break;
                }
            } else {
                switch (color) {
                    case CardColor.Red:
                        uicgGameComponent.EnemyRed.text = num.ToString();
                        uicgGameComponent.EnemyRed.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Blue:
                        uicgGameComponent.EnemyBlue.text = num.ToString();
                        uicgGameComponent.EnemyBlue.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Black:
                        uicgGameComponent.EnemyBlack.text = num.ToString();
                        uicgGameComponent.EnemyBlack.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Green:
                        uicgGameComponent.EnemyGreen.text = num.ToString();
                        uicgGameComponent.EnemyGreen.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Grey:
                        uicgGameComponent.EnemyGrey.text = num.ToString();
                        uicgGameComponent.EnemyGrey.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.White:
                        uicgGameComponent.EnemyWhite.text = num.ToString();
                        uicgGameComponent.EnemyWhite.transform.parent.gameObject.SetActive(true);
                        break;
                }
            }
        }
        
        public static async ETTask Room2C_EnemyGetHandCardsFromGroup(Room room, List<RoomCardInfo> cardInfos) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var cardInfo in cardInfos) {
                UIEnemyHandCardFromGroup(room, uicgGameComponent, cardInfo); 
            }
        }
        
        public static async ETTask Room2C_EnemyGetHandCardFromGroup(Room room, RoomCardInfo cardInfo) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>(); 
            UIEnemyHandCardFromGroup(room, uicgGameComponent, cardInfo);
        }
        
        private static void UIEnemyHandCardFromGroup(Room room, UICGGameComponent uicgGameComponent, RoomCardInfo cardInfo) {
            GameObject handCard = null;
            if (uicgGameComponent.EnemyHandCardPool.Count < 1) {
                handCard = UnityEngine.Object.Instantiate(uicgGameComponent.UIEnemyHandCard, uicgGameComponent.EnemyHandCardsDeck.transform, true);
            } else {
                handCard = uicgGameComponent.EnemyHandCardPool[0];
                uicgGameComponent.EnemyHandCardPool.RemoveAt(0);
            }
            handCard.SetActive(true);
            handCard.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = cardInfo.EnemyGetUIHandCard(room, handCard);
            uicgGameComponent.EnemyHandCards.Add(cardInfo, uiUnit);
        }

        private static void UIGetHandCardFromGroup(Room room, UICGGameComponent uicgGameComponent, RoomCardInfo cardInfo) {
            GameObject handCard = null;
            if (uicgGameComponent.MyHandCardPool.Count < 1) {
                handCard = UnityEngine.Object.Instantiate(uicgGameComponent.UICard, uicgGameComponent.MyHandCardsDeck.transform, true);
            } else {
                handCard = uicgGameComponent.MyHandCardPool[0];
                uicgGameComponent.MyHandCardPool.RemoveAt(0);
            }
            handCard.SetActive(true);
            handCard.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = cardInfo.GetUIHandCard(room, handCard);
            uicgGameComponent.MyHandCards.Add(cardInfo, uiUnit);
        }

        public static async ETTask Room2C_CallUnit(Room room, RoomCardInfo cardInfo, bool isMy) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject unit = null;
            if (uicgGameComponent.UnitPool.Count < 1) {
                unit = UnityEngine.Object.Instantiate(uicgGameComponent.UIUnit, uicgGameComponent.MyUnits.transform, true);
            } else {
                unit = uicgGameComponent.UnitPool[0];
                uicgGameComponent.UnitPool.RemoveAt(0);
            }
            unit.GetComponent<UIUnitDragHandler>().IsMy = isMy;
            unit.SetActive(true);
            unit.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = cardInfo.GetUIUnit(uicgGameComponent, room, unit, isMy);
            if (isMy) {
                uicgGameComponent.MyFightUnits.Add(cardInfo, uiUnit);
            } else {
                uicgGameComponent.EnemyFightUnits.Add(cardInfo, uiUnit);
            }
        }

        public static async ETTask Room2C_CardGetDamage(this Room room, RoomCardInfo cardInfo, int hurt) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            UIUnitInfo info = null;
            foreach (var cardUnitInfo in uicgGameComponent.HeroAndAgent) {
                if (cardInfo.CardId == cardUnitInfo.Key.CardId) {
                    info = cardUnitInfo.Value;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.MyFightUnits) {
                if (cardInfo.CardId == cardUnitInfo.Key.CardId) {
                    info = cardUnitInfo.Value;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.EnemyFightUnits) {
                if (cardInfo.CardId == cardUnitInfo.Key.CardId) {
                    info = cardUnitInfo.Value;
                }
            }

            info.HP.text = cardInfo.HP.ToString();
        }
        
        public static void Room2C_UseHandCards(this UICGGameComponent ui, long CardId) {
            foreach (var card in ui.MyHandCards) {
                if (card.Key.CardId == CardId) {
                    card.Value.CardGo.SetActive(false);
                    ui.UnitPool.Add(card.Value.CardGo);
                    ui.MyFightUnits.Remove(card.Key);
                    return;
                }
            }
        }
        
        public static UIUnitInfo GetUIUnit(this RoomCardInfo cardInfo, UICGGameComponent uicgGameComponent, Room room, GameObject unit, bool isMy) {
            ReferenceCollector rc = unit.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = new UIUnitInfo() {
                CardGo = unit, 
                Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc.Get<GameObject>("Image").GetComponent<Image>(),
            };
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            
            UIUnitDragHandler dragHandler = unit.GetComponent<UIUnitDragHandler>();
            dragHandler.CardId = cardInfo.CardId;
            dragHandler.BaseId = cardInfo.BaseId;
            dragHandler.UIUseCardType = cardInfo.UseCardType;
            dragHandler.UICardType = cardInfo.CardType;
            dragHandler.IsMy = isMy;
            dragHandler.TryToDoInClient = (vector2) => {
                if (unitInfo.Attack.text == "0") {
                    Log.Warning("攻击为0，不能攻击");
                    return false;
                }

                GameObject target = uicgGameComponent.GetAttackTarget(vector2);
                if (target != null) {
                    long targetID = 0;
                    UIUnitDragHandler unitTargetHandler = target.GetComponent<UIUnitDragHandler>();
                    UIHeroDragHandler heroTargetHandler = target.GetComponent<UIHeroDragHandler>();
                    UIAgentDragHandler agentDragHandler = target.GetComponent<UIAgentDragHandler>();
                    if (unitTargetHandler != null)
                        targetID = unitTargetHandler.CardId;
                    if (heroTargetHandler != null)
                        targetID = heroTargetHandler.CardId;
                    if (agentDragHandler != null)
                        targetID = agentDragHandler.CardId;
                    if (targetID != 0) {
                        dragHandler.UseCardToServer.Invoke(targetID);
                        return true;
                    }
                } 
                return false;
            };
            dragHandler.UseCardToServer = (target) => {
                room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_Attack() { Actor = cardInfo.CardId, Target = target});
            };
            dragHandler.ShowUIShowCard = () => {
                bool left = dragHandler.gameObject.transform.position.x < 0;
                uicgGameComponent.ShowUIShowCard(left, dragHandler.BaseId);
            };
            
            return unitInfo;
        }

        public static UIUnitInfo EnemyGetUIHandCard(this RoomCardInfo cardInfo, Room room, GameObject card) {
            UIUnitInfo unitInfo = new UIUnitInfo() {
                CardGo = card,
            };
            return unitInfo;
        }
        
        public static UIUnitInfo GetUIHandCard(this RoomCardInfo cardInfo, Room room, GameObject card) {
            ReferenceCollector rc = card.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = new UIUnitInfo() {
                CardGo = card, 
                Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc.Get<GameObject>("Image").GetComponent<Image>(),
                Cost = rc.Get<GameObject>("Cost").GetComponent<Text>(),
                Info = rc.Get<GameObject>("Info").GetComponent<Text>(),
                Name = rc.Get<GameObject>("Name").GetComponent<Text>(),
                // Color
                Red = rc.Get<GameObject>("Red").GetComponentInChildren<Text>(),
                Blue = rc.Get<GameObject>("Blue").GetComponentInChildren<Text>(),
                Green = rc.Get<GameObject>("Green").GetComponentInChildren<Text>(),
                White = rc.Get<GameObject>("White").GetComponentInChildren<Text>(),
                Grey = rc.Get<GameObject>("Grey").GetComponentInChildren<Text>(),
                Black = rc.Get<GameObject>("Black").GetComponentInChildren<Text>(),
            };
            unitInfo.Name.text = CardConfigCategory.Instance.Get(cardInfo.BaseId).Name;
            unitInfo.Info.text = CardConfigCategory.Instance.Get(cardInfo.BaseId).Desc;
            
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            unitInfo.Cost.text = cardInfo.Cost.ToString();
            SetHandCardColor(unitInfo.Red, cardInfo.Red);
            SetHandCardColor(unitInfo.Blue, cardInfo.Blue);
            SetHandCardColor(unitInfo.Green, cardInfo.Green);
            SetHandCardColor(unitInfo.White, cardInfo.White);
            SetHandCardColor(unitInfo.Grey, cardInfo.Grey);
            SetHandCardColor(unitInfo.Black, cardInfo.Black);
            
            UIHandCardDragHandler dragHandler = card.GetComponent<UIHandCardDragHandler>();
            dragHandler.CardId = cardInfo.CardId;
            dragHandler.UIUseCardType = (UIUseCardType)cardInfo.UseCardType;
            dragHandler.UICardType = (UICardType)cardInfo.CardType;
            dragHandler.TryToDoInClient = (posF) => {
                if ((int)dragHandler.UIUseCardType == (int)UseCardType.ToUnit ||
                    (int)dragHandler.UIUseCardType == (int)UseCardType.ToHero) {
                    long target = dragHandler.GetTarget();
                    if (target != 0) {
                        dragHandler.UseCardToServer.Invoke(target, 0);
                    } else {
                        return false;
                    }
                } else if ((int)dragHandler.UICardType == (int)CardType.Unit &&
                           (int)dragHandler.UIUseCardType == (int)UseCardType.NoTarget) {
                    dragHandler.UseCardToServer.Invoke(0, 0);
                }
                return false;
            };
            dragHandler.UseCardToServer = (target, i) => {
                room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = target, Pos = 0 });
            };
            
            return unitInfo;
        }

        public static async ETTask Room2C_EnemyNewHero(Entity room, RoomCardInfo cardInfo) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject hero = uicgGameComponent.EnemyHero;
            ReferenceCollector rc = hero.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = new UIUnitInfo() {
                CardGo = hero, 
                Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc.Get<GameObject>("Image").GetComponent<Image>(),
            };
            
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            UIHeroDragHandler dragHandler = hero.GetComponent<UIHeroDragHandler>();
            dragHandler.CardId = cardInfo.CardId;
            dragHandler.IsMy = false;
            uicgGameComponent.HeroAndAgent.Add(cardInfo, unitInfo);
            await ETTask.CompletedTask;
        }
        
        public static async ETTask Room2C_NewHero(Entity room, RoomCardInfo cardInfo) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject hero = uicgGameComponent.MyHero;
            ReferenceCollector rc = hero.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = new UIUnitInfo() {
                CardGo = hero, 
                Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc.Get<GameObject>("Image").GetComponent<Image>(),
            };
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            UIHeroDragHandler dragHandler = hero.GetComponent<UIHeroDragHandler>();
            dragHandler.CardId = cardInfo.CardId;
            dragHandler.IsMy = true;
            uicgGameComponent.HeroAndAgent.Add(cardInfo, unitInfo);
            await ETTask.CompletedTask;
        }
        
        public static async ETTask Room2Cost_MyCost(Entity room, int cost, int costMax) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            uicgGameComponent.MyCost.text = cost.ToString() + "/" + costMax.ToString();
            await ETTask.CompletedTask;
        }
        
        public static async ETTask Room2Cost_EnemyCost(Entity room, int cost, int costMax) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            uicgGameComponent.EnemyCost.text = cost.ToString() + "/" + costMax.ToString();
            await ETTask.CompletedTask;
        }

        public static async ETTask Room2C_NewAgentType(Entity room, RoomCardInfo agentCard1, RoomCardInfo agentCard2, bool isMy) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject agent1 = isMy ? uicgGameComponent.MyAgent1 : uicgGameComponent.EnemyAgent1;
            GameObject agent2 = isMy ? uicgGameComponent.MyAgent2 : uicgGameComponent.EnemyAgent2;
            //Agent1
            ReferenceCollector rc1 = agent1.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo1 = new UIUnitInfo() {
                CardGo = agent1, 
                Attack = rc1.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc1.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc1.Get<GameObject>("Image").GetComponent<Image>(),
            };
            unitInfo1.Attack.text = agentCard1.Attack.ToString();
            unitInfo1.HP.text = agentCard1.HP.ToString();
            UIAgentDragHandler dragHandler1 = agent1.GetComponent<UIAgentDragHandler>();
            dragHandler1.CardId = agentCard1.CardId;
            dragHandler1.IsMy = isMy;
            uicgGameComponent.HeroAndAgent.Add(agentCard1, unitInfo1);
            //Agent2
            ReferenceCollector rc2 = agent1.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo2 = new UIUnitInfo() {
                CardGo = agent2, 
                Attack = rc2.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = rc2.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = rc2.Get<GameObject>("Image").GetComponent<Image>(),
            };
            unitInfo2.Attack.text = agentCard2.Attack.ToString();
            Log.Warning(unitInfo2.Attack.text);
            unitInfo2.HP.text = agentCard2.HP.ToString();
            UIAgentDragHandler dragHandler2 = agent1.GetComponent<UIAgentDragHandler>();
            dragHandler2.CardId = agentCard2.CardId;
            dragHandler2.IsMy = isMy;
            uicgGameComponent.HeroAndAgent.Add(agentCard2, unitInfo2);
            await ETTask.CompletedTask;
        }
        
        public static async ETTask TurnStart(Entity room,  TurnStart turnStart) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            if (turnStart.IsThisClient) {
                //展示回合开始UI
                uicgGameComponent.IsShowTurnStart = true;

                uicgGameComponent.MyCost.text = turnStart.Cost.ToString() + "/" + turnStart.CostD.ToString();
                uicgGameComponent.MyRed.text = turnStart.Red.ToString();
                uicgGameComponent.MyBlue.text = turnStart.Blue.ToString();
                uicgGameComponent.MyGreen.text = turnStart.Green.ToString();
                uicgGameComponent.MyWhite.text = turnStart.White.ToString();
                uicgGameComponent.MyBlack.text = turnStart.Black.ToString();
                uicgGameComponent.MyGrey.text = turnStart.Grey.ToString();
            } else {
                uicgGameComponent.EnemyCost.text = turnStart.Cost.ToString() + "/" + turnStart.CostD.ToString();
                uicgGameComponent.EnemyRed.text = turnStart.Red.ToString();
                uicgGameComponent.EnemyBlue.text = turnStart.Blue.ToString();
                uicgGameComponent.EnemyGreen.text = turnStart.Green.ToString();
                uicgGameComponent.EnemyWhite.text = turnStart.White.ToString();
                uicgGameComponent.EnemyBlack.text = turnStart.Black.ToString();
                uicgGameComponent.EnemyGrey.text = turnStart.Grey.ToString();
            }
        }

        public static async ETTask C2Room_TurnOver(Scene root) {
            root.GetComponent<ClientSenderCompnent>().Send(new C2Room_TurnOver());
            await ETTask.CompletedTask;
        }

        public static void SetHandCardColor(Text text, int num) {
            if (num < 1) {
                text.transform.parent.gameObject.SetActive(false);
            } else {
                text.transform.parent.gameObject.SetActive(true);
                text.text = num.ToString();
            }
        }
    }
}