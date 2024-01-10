using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UICGGameHelper
    {

        public static void RemoveMyUnit(this UICGGameComponent ui, long CardId)
        {
            ui.RemoveUnit(CardId, true);
        }
        public static void RemoveEnemyUnit(this UICGGameComponent ui, long CardId)
        {
            ui.RemoveUnit(CardId, false);
        }

        private static void RemoveUnit(this UICGGameComponent ui, long CardId, bool IsMy)
        {
            var units = IsMy ? ui.MyFightUnits : ui.EnemyFightUnits;
            foreach (var unit in units)
            {
                if (unit.CardId == CardId)
                {
                    unit.CardGo.SetActive(false);
                    ui.UnitPool.Add(unit.CardGo);
                    ui.MyFightUnits.Remove(unit);
                    return;
                }
            }
        }

        public static async ETTask Room2C_LoseHandCard(Room room, long cardId)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var handCard in uicgGameComponent.MyHandCards)
            {
                if (cardId == handCard.CardId)
                {
                    uicgGameComponent.MyHandCardPool.Add(handCard.CardGo);
                    handCard.CardGo.SetActive(false);
                    uicgGameComponent.MyHandCards.Remove(handCard);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(handCard.Id);
                    uicgGameComponent.HandCardsPos();
                    return;
                }
            }
            foreach (var handCard in uicgGameComponent.EnemyHandCards)
            {
                if (cardId == handCard.CardId)
                {
                    uicgGameComponent.EnemyHandCardPool.Add(handCard.CardGo);
                    handCard.CardGo.SetActive(false);
                    uicgGameComponent.EnemyHandCards.Remove(handCard);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(handCard.Id);
                    uicgGameComponent.EnemyHandCardsPos();
                    return;
                }
            }
        }

        public static async ETTask Room2C_GetHandCardFromGroup(Room room, RoomCardInfo cardInfo)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await UIGetHandCardFromGroup(room, uicgGameComponent, cardInfo);
        }

        public static async ETTask Room2C_GetColor(Room room, CardColor color, int num, bool isMy)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            if (isMy)
            {
                switch (color)
                {
                    case CardColor.Red:
                        uicgGameComponent.MyRed.text = num.ToString();
                        uicgGameComponent.DMyRed = num;
                        uicgGameComponent.MyRed.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Blue:
                        uicgGameComponent.MyBlue.text = num.ToString();
                        uicgGameComponent.DMyBlue = num;
                        uicgGameComponent.MyBlue.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Black:
                        uicgGameComponent.MyBlack.text = num.ToString();
                        uicgGameComponent.DMyBlack = num;
                        uicgGameComponent.MyBlack.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Green:
                        uicgGameComponent.MyGreen.text = num.ToString();
                        uicgGameComponent.DMyGreen = num;
                        uicgGameComponent.MyGreen.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.Grey:
                        uicgGameComponent.MyGrey.text = num.ToString();
                        uicgGameComponent.DMyGrey = num;
                        uicgGameComponent.MyGrey.transform.parent.gameObject.SetActive(true);
                        break;
                    case CardColor.White:
                        uicgGameComponent.MyWhite.text = num.ToString();
                        uicgGameComponent.DMyWhite = num;
                        uicgGameComponent.MyWhite.transform.parent.gameObject.SetActive(true);
                        break;
                }
            }
            else
            {
                switch (color)
                {
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

        public static async ETTask Room2C_EnemyGetHandCardsFromGroup(Room room, List<RoomCardInfo> cardInfos)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var cardInfo in cardInfos)
            {
                UIEnemyHandCardFromGroup(room, uicgGameComponent, cardInfo);
            }
        }

        public static async ETTask Room2C_EnemyGetHandCardFromGroup(Room room, RoomCardInfo cardInfo)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            UIEnemyHandCardFromGroup(room, uicgGameComponent, cardInfo);
        }

        private static void UIEnemyHandCardFromGroup(Room room, UICGGameComponent uicgGameComponent, RoomCardInfo cardInfo)
        {
            GameObject handCard = null;
            if (uicgGameComponent.EnemyHandCardPool.Count < 1)
            {
                handCard = UnityEngine.Object.Instantiate(uicgGameComponent.UIEnemyHandCard, uicgGameComponent.EnemyHandCardsDeck.transform, true);
            }
            else
            {
                handCard = uicgGameComponent.EnemyHandCardPool[0];
                uicgGameComponent.EnemyHandCardPool.RemoveAt(0);
            }
            handCard.SetActive(true);
            handCard.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = cardInfo.EnemyGetUIHandCard(uicgGameComponent, room, handCard);
            uicgGameComponent.EnemyHandCards.Add(uiUnit);
        }

        private static async ETTask UIGetHandCardFromGroup(Room room, UICGGameComponent uicgGameComponent, RoomCardInfo cardInfo)
        {
            GameObject handCard = null;
            if (uicgGameComponent.MyHandCardPool.Count < 1)
            {
                handCard = UnityEngine.Object.Instantiate(uicgGameComponent.UICard, uicgGameComponent.MyHandCardsDeck.transform, true);
            }
            else
            {
                handCard = uicgGameComponent.MyHandCardPool[0];
                uicgGameComponent.MyHandCardPool.RemoveAt(0);
            }
            handCard.SetActive(true);
            handCard.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = await uicgGameComponent.CreateUIHandCard(cardInfo, room, handCard);
        }

        public static async ETTask Room2C_RoomCardAttack(Room room, long actorId, long targetId) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UIAnimComponent animComponent = ui.GetComponent<UICGGameComponent>().GetComponent<UIAnimComponent>();
            UIUnitInfo actor = null, target = null;
            foreach (UIUnitInfo unitInfo in animComponent.Children.Values) {
                if (unitInfo.CardId == actorId)
                    actor = unitInfo;
                if (unitInfo.CardId == targetId)
                    target = unitInfo;
            } 
            actor.AttackTo(target);
        }

        public static async ETTask Room2C_CallUnit(Room room, RoomCardInfo cardInfo, bool isMy)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject unit = null;
            if (uicgGameComponent.UnitPool.Count < 1)
            {
                unit = UnityEngine.Object.Instantiate(uicgGameComponent.UIUnit,
                    isMy ? uicgGameComponent.MyUnits.transform : uicgGameComponent.EnemyUnits.transform, true);
            }
            else
            {
                unit = uicgGameComponent.UnitPool[0];
                uicgGameComponent.UnitPool.RemoveAt(0);
            }
            unit.GetComponent<UIUnitDragHandler>().IsMy = isMy;
            unit.SetActive(true);
            unit.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = await cardInfo.GetUIUnit(uicgGameComponent, room, unit, isMy);
        }

        public static async ETTask Room2C_CardGetDamage(this Room room, RoomCardInfo cardInfo, int hurt)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            UIUnitInfo info = null;
            foreach (var cardUnitInfo in uicgGameComponent.HeroAndAgent)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.MyFightUnits)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.EnemyFightUnits)
            {
                if (cardInfo.CardId == cardUnitInfo.CardId)
                {
                    info = cardUnitInfo;
                }
            }
            info.AppendCallback(() => info.HP.text = cardInfo.HP.ToString());
        }

        public static void Room2C_UseHandCards(this UICGGameComponent ui, long CardId)
        {
            foreach (var card in ui.MyHandCards)
            {
                if (card.CardId == CardId)
                {
                    card.CardGo.SetActive(false);
                    ui.UnitPool.Add(card.CardGo);
                    ui.MyFightUnits.Remove(card);
                    return;
                }
            }
        }

        public static async ETTask OrderUnits(this Room room, List<long> unitsOrder)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            List<UIUnitInfo> newUnits = new List<UIUnitInfo>();
            foreach (var unit in unitsOrder)
            {
                newUnits.Add(uicgGameComponent.GetMyUnit(unit));
            }
            uicgGameComponent.MyFightUnits = newUnits;
        }

        public static UIUnitInfo GetMyUnit(this UICGGameComponent ui, long cardId)
        {
            foreach (var units in ui.MyFightUnits)
            {
                if (cardId == units.CardId)
                {
                    return units;
                }
            }
            return null;
        }

        public static async ETTask<UIUnitInfo> GetUIUnit(this RoomCardInfo cardInfo, UICGGameComponent uicgGameComponent, Room room, GameObject unit, bool isMy)
        {
            ReferenceCollector rc = unit.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(unit);
            unitInfo.CardGo = unit;
            unitInfo.CardId = cardInfo.CardId;
            unitInfo.BaseId = cardInfo.BaseId;
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            unitInfo.Taunt = rc.Get<GameObject>("Taunt");
            if (isMy)
            {
                uicgGameComponent.MyFightUnits.Add(unitInfo);
            }
            else
            {
                uicgGameComponent.EnemyFightUnits.Add(unitInfo);
            }
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            unitInfo.Taunt.SetActive(cardInfo.CardPowers.Contains((int)Power_Type.Taunt));
            
            //参数
            unitInfo.DAttack = cardInfo.Attack;
            unitInfo.DHP = cardInfo.HP;
            unitInfo.DCost = cardInfo.Cost;

            //Sprite
            string spritePath = $"Assets/Bundles/CardImage/{cardInfo.BaseId}.png";
            Sprite sprite = await room.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath);
            unitInfo.Image.sprite = sprite;

            UIUnitDragHandler dragHandler = unit.GetComponent<UIUnitDragHandler>();
            dragHandler.IsMy = isMy;
            dragHandler.IsDrag = (b) => {
                unitInfo.IsDrag = b;
            };
            dragHandler.CanBeUsed = () => {
                if (unitInfo.DAttack < 1) {
                    Log.Warning("????");
                    return false;
                }

                if (!unitInfo.AttackCountEnough) {
                    Log.Warning("????");
                    return false;
                }
                Log.Warning("????");

                return true;
            };
            dragHandler.DragShow = v => {
                GameObject target = uicgGameComponent.GetAttackTarget(v);
                if (target != null) {
                    unitInfo.TargetInfo = uicgGameComponent.GetComponent<UIAnimComponent>().GetUnitInfoByGo(target);
                    target.transform.localScale = new Vector3(1.05f, 1.05f);
                } else if (unitInfo.TargetInfo != null) {
                    unitInfo.TargetInfo.CardGo.transform.localScale = Vector3.one;
                    unitInfo.TargetInfo = null;
                }
            };
            dragHandler.TryToDoInClient = (vector2) =>
            {
                if (unitInfo.TargetInfo != null) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_Attack() { Actor = cardInfo.CardId, Target = unitInfo.TargetInfo.CardId });
                    return true;
                }
                return false;
            };
            dragHandler.ShowUIShowCard = () =>
            {
                bool left = dragHandler.gameObject.transform.position.x < 30;
                uicgGameComponent.ShowUIShowCard(left, unitInfo.BaseId).Coroutine();
            };
            dragHandler.HideUIShowCard = uicgGameComponent.HideUIShowCard;

            return unitInfo;
        }

        public static UIUnitInfo EnemyGetUIHandCard(this RoomCardInfo cardInfo, UICGGameComponent ui, Room room, GameObject card)
        {
            UIUnitInfo unitInfo = ui.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(card);
            unitInfo.CardId = cardInfo.CardId;
            return unitInfo;
        }

        public static async ETTask<UIUnitInfo> CreateUIHandCard(this UICGGameComponent ui, RoomCardInfo cardInfo, Room room, GameObject card)
        {
            ReferenceCollector rc = card.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = ui.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(card);
            unitInfo.CardGo = card;
            unitInfo.CardId = cardInfo.CardId;
            unitInfo.BaseId = cardInfo.BaseId;
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            unitInfo.Cost = rc.Get<GameObject>("Cost").GetComponentInChildren<Text>();
            unitInfo.Info = rc.Get<GameObject>("Info").GetComponent<Text>();
            unitInfo.Name = rc.Get<GameObject>("Name").GetComponent<Text>();
            // Color
            unitInfo.Red = rc.Get<GameObject>("Red").GetComponentInChildren<Text>();
            unitInfo.Blue = rc.Get<GameObject>("Blue").GetComponentInChildren<Text>();
            unitInfo.Green = rc.Get<GameObject>("Green").GetComponentInChildren<Text>();
            unitInfo.White = rc.Get<GameObject>("White").GetComponentInChildren<Text>();
            unitInfo.Grey = rc.Get<GameObject>("Grey").GetComponentInChildren<Text>();
            unitInfo.Black = rc.Get<GameObject>("Black").GetComponentInChildren<Text>();
            
            unitInfo.CardType = (CardType)cardInfo.CardType;
            unitInfo.UseCardType = (UseCardType)cardInfo.UseCardType;
            
            ui.MyHandCards.Add(unitInfo);
            ui.HandCardsPos();
            unitInfo.Name.text = CardConfigCategory.Instance.Get(cardInfo.BaseId).Name;
            unitInfo.Info.text = CardConfigCategory.Instance.Get(cardInfo.BaseId).Desc;
            //Sprite
            string spritePath = $"Assets/Bundles/CardImage/{cardInfo.BaseId}.png";
            Sprite sprite = await room.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath);
            unitInfo.Image.sprite = sprite;

            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            unitInfo.Cost.text = cardInfo.Cost.ToString();
            unitInfo.DCost = cardInfo.Cost;
            unitInfo.DAttack = cardInfo.Attack;
            unitInfo.DHP = cardInfo.HP;
            unitInfo.DRed = cardInfo.Red;
            unitInfo.DBlue = cardInfo.Blue;
            unitInfo.DGreen = cardInfo.Green;
            unitInfo.DGrey = cardInfo.Grey;
            unitInfo.DBlack = cardInfo.Black;
            unitInfo.DWhite = cardInfo.White;
            SetHandCardColor(unitInfo.Red, cardInfo.Red);
            SetHandCardColor(unitInfo.Blue, cardInfo.Blue);
            SetHandCardColor(unitInfo.Green, cardInfo.Green);
            SetHandCardColor(unitInfo.White, cardInfo.White);
            SetHandCardColor(unitInfo.Grey, cardInfo.Grey);
            SetHandCardColor(unitInfo.Black, cardInfo.Black);

            //手牌拖拽脚本
            UIHandCardDragHandler dragHandler = card.GetComponent<UIHandCardDragHandler>();
            dragHandler.IsMy = true;
            dragHandler.IsDragEnable = () => {
                if (unitInfo.CardType == CardType.Plot && unitInfo.UseCardType == UseCardType.NoTarget) {
                    if (unitInfo.CardGo.transform.position.y - unitInfo.TargetPos.y > 20f) {
                        return true;
                    }
                } else if (unitInfo.CardType == CardType.Magic && unitInfo.UseCardType == UseCardType.NoTarget) {
                    if (unitInfo.CardGo.transform.position.y - unitInfo.TargetPos.y > 20f) {
                        return true;
                    }
                } else if (unitInfo.CardType == CardType.Unit && unitInfo.UseCardType == UseCardType.NoTarget) {
                    if(unitInfo.CardGo.transform.position.y - unitInfo.TargetPos.y > 20f) {
                        return true;
                    }
                } else if (unitInfo.CardType == CardType.Magic && unitInfo.UseCardType == UseCardType.ToActor) {
                    return true;
                }

                return false;
            };
            dragHandler.BeSelect = b => {
                if (!b) {
                    unitInfo.IsBeSelect = false;
                    unitInfo.TargetScale = 1f;
                    for (int i = 0; i < ui.MyHandCards.Count; i++) {
                        if (ui.MyHandCards[i].CardId == unitInfo.CardId) {
                            if (i == ui.SelectCardPos) {
                                ui.SelectCardPos = -1;
                                return;
                            }
                        }
                    }
                } else {
                    unitInfo.IsBeSelect = true;
                    unitInfo.TargetScale = 1.3f;
                    if (ui.SelectCardPos != -1)
                        return;
                    for (int i = 0; i < ui.MyHandCards.Count; i++) {
                        if (ui.MyHandCards[i].CardId == unitInfo.CardId) {
                            ui.SelectCardPos = i;
                            return;
                        }
                    }
                }
            };
            dragHandler.IsBeDrag = b => {
                unitInfo.IsDrag = b;
            };
            dragHandler.IsUnitInDrag = () => unitInfo.CardType == CardType.Unit;
            dragHandler.GetHeroVector = () => ui.MyHero.transform.position;
            dragHandler.GetTargetPos = () => unitInfo.TargetPos;
            dragHandler.CanBeUsed = () =>
            {
                //费用不足，无法使用
                if (unitInfo.DCost > ui.DMyCost)
                {
                    return false;
                }
                return true;
            };
            dragHandler.IsToTargetInDrag = v => {
                if (unitInfo.UseCardType == UseCardType.NoTarget) {
                    unitInfo.TargetInfo = null;
                    return false;
                } else {
                    GameObject target = ui.GetAttackTarget(v);
                    if (target != null) {
                        unitInfo.TargetInfo = ui.GetComponent<UIAnimComponent>().GetUnitInfoByGo(target);
                        target.transform.localScale = new Vector3(1.05f, 1.05f);
                    } else if (unitInfo.TargetInfo != null) {
                        unitInfo.TargetInfo.CardGo.transform.localScale = Vector3.one;
                        unitInfo.TargetInfo = null;
                    }

                    return true;
                }
            };
            dragHandler.CardPos = (vector3) => {
                if (unitInfo.CardType != CardType.Unit) return;
                if (vector3.x > 9999f) {
                    ui.MyHandCardPos = -1;
                } else {
                    int i = 0;
                    foreach (var unit in ui.MyFightUnits) {
                        if (unit.CardGo.transform.position.x > vector3.x) {
                            continue;
                        }
                        i++;
                    }
                    ui.MyHandCardPos = i;
                }
            };
            dragHandler.TryToDoInClient = (vector2) => {
                ui.SelectCardPos = -1;
                if ((unitInfo.CardType == CardType.Magic || unitInfo.CardType == CardType.Plot) && 
                    (unitInfo.UseCardType == UseCardType.ToUnit || 
                        unitInfo.UseCardType == UseCardType.ToHero ||
                        unitInfo.UseCardType == UseCardType.ToActor)) {
                    if (unitInfo.TargetInfo != null) {
                        room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = unitInfo.TargetInfo.CardId, Pos = ui.MyHandCardPos });
                        return true;
                    }
                } else if (unitInfo.CardType == CardType.Unit &&
                           unitInfo.UseCardType == UseCardType.NoTarget) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = 0, Pos = ui.MyHandCardPos });
                } else if ((unitInfo.CardType == CardType.Magic || unitInfo.CardType == CardType.Plot) &&
                           unitInfo.UseCardType == UseCardType.NoTarget) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = 0, Pos = 0 });
                }
                return false;
            };

            return unitInfo;
        }

        public static async ETTask Room2C_EnemyNewHero(Entity room, RoomCardInfo cardInfo)
        {
            await NewHero(room, cardInfo, false);
        }

        public static async ETTask Room2C_NewHero(Entity room, RoomCardInfo cardInfo)
        {
            await NewHero(room, cardInfo, true);
        }

        private static async ETTask NewHero(Entity room, RoomCardInfo cardInfo, bool isMy)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject hero = isMy ? uicgGameComponent.MyHero : uicgGameComponent.EnemyHero;
            ReferenceCollector rc = hero.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(hero);
            unitInfo.CardId = cardInfo.CardId;
            unitInfo.BaseId = cardInfo.BaseId;
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo);
            //Sprite
            string spritePath = $"Assets/Bundles/CardImage/{cardInfo.BaseId}.png";
            try
            {
                Sprite sprite = await (room as Room).GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath);
                unitInfo.Image.sprite = sprite;
            }
            catch
            {
                Log.Warning($"{spritePath}还未引入匹配图片");
            }

            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
            UIHeroDragHandler dragHandler = hero.GetComponent<UIHeroDragHandler>();
            dragHandler.CardId = cardInfo.CardId;
            dragHandler.BaseId = cardInfo.BaseId;
            dragHandler.IsMy = isMy;
            dragHandler.ShowUIShowCard = () =>
            {
                bool left = dragHandler.gameObject.transform.position.x < 30;
                uicgGameComponent.ShowUIShowCard(left, dragHandler.BaseId).Coroutine();
            };
            dragHandler.HideUIShowCard = uicgGameComponent.HideUIShowCard;
            await ETTask.CompletedTask;
        }

        public static async ETTask Room2Cost_MyCost(Entity room, int cost, int costMax)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            uicgGameComponent.DMyCost = cost;
            uicgGameComponent.MyCost.text = cost.ToString() + "/" + costMax.ToString();
            await ETTask.CompletedTask;
        }

        public static async ETTask Room2Cost_EnemyCost(Entity room, int cost, int costMax)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            uicgGameComponent.EnemyCost.text = cost.ToString() + "/" + costMax.ToString();
            await ETTask.CompletedTask;
        }

        public static async ETTask Room2C_NewAgentType(Entity room, RoomCardInfo agentCard1, RoomCardInfo agentCard2, bool isMy)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject agent1 = isMy ? uicgGameComponent.MyAgent1 : uicgGameComponent.EnemyAgent1;
            GameObject agent2 = isMy ? uicgGameComponent.MyAgent2 : uicgGameComponent.EnemyAgent2;
            //Agent1
            ReferenceCollector rc1 = agent1.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo1 = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(agent1);
            unitInfo1.CardGo = agent1;
            unitInfo1.Attack = rc1.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo1.HP = rc1.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo1.Image = rc1.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo1);
            unitInfo1.Attack.text = agentCard1.Attack.ToString();
            unitInfo1.HP.text = agentCard1.HP.ToString();
            string spritePath1 = $"Assets/Bundles/CardImage/{agentCard1.BaseId}.png";
            try
            {
                Sprite sprite = await (room as Room).GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath1);
                unitInfo1.Image.sprite = sprite;
            }
            catch
            {
                Log.Warning($"{spritePath1}还未引入匹配图片");
            }
            UIAgentDragHandler dragHandler1 = agent1.GetComponent<UIAgentDragHandler>();
            dragHandler1.CardId = agentCard1.CardId;
            dragHandler1.IsMy = isMy;
            dragHandler1.BaseId = agentCard1.BaseId;
            dragHandler1.ShowUIShowCard = () =>
            {
                bool left = dragHandler1.gameObject.transform.position.x < 30;
                uicgGameComponent.ShowUIShowCard(left, dragHandler1.BaseId).Coroutine();
            };
            dragHandler1.HideUIShowCard = uicgGameComponent.HideUIShowCard;
            //Agent2
            ReferenceCollector rc2 = agent2.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo2 = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(agent2);
            unitInfo2.Attack = rc2.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo2.HP = rc2.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo2.Image = rc2.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo2);
            unitInfo2.Attack.text = agentCard2.Attack.ToString();
            unitInfo2.HP.text = agentCard2.HP.ToString();
            string spritePath2 = $"Assets/Bundles/CardImage/{agentCard2.BaseId}.png";
            try
            {
                Sprite sprite = await (room as Room).GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath2);
                unitInfo2.Image.sprite = sprite;
            }
            catch
            {
                Log.Warning($"{spritePath2}还未引入匹配图片");
            }
            UIAgentDragHandler dragHandler2 = agent2.GetComponent<UIAgentDragHandler>();
            dragHandler2.CardId = agentCard2.CardId;
            dragHandler2.IsMy = isMy;
            dragHandler2.BaseId = agentCard2.BaseId;
            dragHandler2.ShowUIShowCard = () =>
            {
                bool left = dragHandler2.gameObject.transform.position.x < 30;
                uicgGameComponent.ShowUIShowCard(left, dragHandler2.BaseId).Coroutine();
            };
            dragHandler2.HideUIShowCard = uicgGameComponent.HideUIShowCard;
            await ETTask.CompletedTask;
        }

        public static async ETTask TurnStart(Entity room, TurnStart turnStart)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            if (turnStart.IsThisClient)
            {
                //展示回合开始UI
                uicgGameComponent.IsShowTurnStart = true;

                uicgGameComponent.MyCost.text = turnStart.Cost.ToString() + "/" + turnStart.CostD.ToString();
                uicgGameComponent.DMyCost = turnStart.Cost;
                uicgGameComponent.MyRed.text = turnStart.Red.ToString();
                uicgGameComponent.MyBlue.text = turnStart.Blue.ToString();
                uicgGameComponent.MyGreen.text = turnStart.Green.ToString();
                uicgGameComponent.MyWhite.text = turnStart.White.ToString();
                uicgGameComponent.MyBlack.text = turnStart.Black.ToString();
                uicgGameComponent.MyGrey.text = turnStart.Grey.ToString();
            }
            else
            {
                uicgGameComponent.EnemyCost.text = turnStart.Cost.ToString() + "/" + turnStart.CostD.ToString();
                uicgGameComponent.EnemyRed.text = turnStart.Red.ToString();
                uicgGameComponent.EnemyBlue.text = turnStart.Blue.ToString();
                uicgGameComponent.EnemyGreen.text = turnStart.Green.ToString();
                uicgGameComponent.EnemyWhite.text = turnStart.White.ToString();
                uicgGameComponent.EnemyBlack.text = turnStart.Black.ToString();
                uicgGameComponent.EnemyGrey.text = turnStart.Grey.ToString();
            }
        }

        public static async ETTask UnitDead(Entity room, long cardId) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var unit in uicgGameComponent.MyFightUnits) {
                if (unit.CardId == cardId) {
                    uicgGameComponent.MyFightUnits.Remove(unit);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.UnitPool.Add(unit.CardGo);
                    unit.CardGo.SetActive(false);
                    return;
                }
            }
            foreach (var unit in uicgGameComponent.EnemyFightUnits) {
                if (unit.CardId == cardId) {
                    uicgGameComponent.MyFightUnits.Remove(unit);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.UnitPool.Add(unit.CardGo);
                    unit.CardGo.SetActive(false);
                    return;
                }
            }
        }

        public static async ETTask C2Room_TurnOver(Scene root)
        {
            root.GetComponent<ClientSenderCompnent>().Send(new C2Room_TurnOver());
            await ETTask.CompletedTask;
        }

        public static void SetHandCardColor(Text text, int num)
        {
            if (num < 1)
            {
                text.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                text.transform.parent.gameObject.SetActive(true);
                text.text = num.ToString();
            }
        }
    }
}