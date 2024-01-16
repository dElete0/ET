using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UICGGameHelper
    {
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

        public static async ETTask Room2C_GetHandCards(Room room, List<RoomCardInfo> cardInfos) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await UIGetHandCards(room, uicgGameComponent, cardInfos);
        }
        
        public static async ETTask Room2C_EnemyGetHandCards(Room room, List<RoomCardInfo> cardInfos) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            UIEnemyGetHandCards(room, uicgGameComponent, cardInfos);
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
        
        private static void UIEnemyGetHandCards(Room room, UICGGameComponent uicgGameComponent, List<RoomCardInfo> cardInfos)
        {
            foreach (var cardInfo in cardInfos) {
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
        }

        private static async ETTask UIGetHandCards(Room room, UICGGameComponent uicgGameComponent, List<RoomCardInfo> cardInfos) {
            foreach (var cardInfo in cardInfos) {
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

        public static async ETTask CallUnit(this Room room, RoomCardInfo cardInfo, bool isMy)
        {
            //Log.Warning($"执行单位上场逻辑:{cardInfo.CardId}");
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
                unit.transform.SetParent(isMy ? uicgGameComponent.MyUnits.transform : uicgGameComponent.EnemyUnits.transform);
                uicgGameComponent.UnitPool.RemoveAt(0);
            }
            Log.Warning($"创建单位:{cardInfo.CardId}");
            unit.GetComponent<UIUnitDragHandler>().IsMy = isMy;
            unit.SetActive(true);
            unit.transform.localScale = Vector3.one;
            UIUnitInfo uiUnit = await cardInfo.GetUIUnit(uicgGameComponent, room, unit, isMy);
        }
        
        public static async ETTask CallUnits(this Room room, List<RoomCardInfo> cardInfos, bool isMy)
        {
            //Log.Warning($"执行单位上场逻辑:{cardInfo.CardId}");
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var cardInfo in cardInfos) {
                GameObject unit = null;
                if (uicgGameComponent.UnitPool.Count < 1)
                {
                    unit = UnityEngine.Object.Instantiate(uicgGameComponent.UIUnit,
                        isMy ? uicgGameComponent.MyUnits.transform : uicgGameComponent.EnemyUnits.transform, true);
                }
                else
                {
                    unit = uicgGameComponent.UnitPool[0];
                    unit.transform.SetParent(isMy ? uicgGameComponent.MyUnits.transform : uicgGameComponent.EnemyUnits.transform);
                    uicgGameComponent.UnitPool.RemoveAt(0);
                }
                unit.GetComponent<UIUnitDragHandler>().IsMy = isMy;
                unit.SetActive(true);
                unit.transform.localScale = Vector3.one;
                UIUnitInfo uiUnit = await cardInfo.GetUIUnit(uicgGameComponent, room, unit, isMy);
            }
        }

        public static async ETTask OrderUnits(this Room room, List<long> unitsOrder, bool IsMy)
        {
            if (unitsOrder == null) Log.Error($"客户端收到的排序消息为空:{unitsOrder == null}");
            //Log.Warning($"客户端收到排序消息:{unitsOrder.Count}");
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            List<UIUnitInfo> newUnits = new List<UIUnitInfo>();
            foreach (var unit in unitsOrder) {
                newUnits.Add(uicgGameComponent.GetUIUnitInfoById(unit));
            }

            if (IsMy) {
                uicgGameComponent.MyFightUnits = newUnits;
            } else {
                uicgGameComponent.EnemyFightUnits = newUnits;
            }
        }
        
        public static async ETTask ShowCardGetDamage(this UIUnitInfo unitInfo, Room room, int damage)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            GameObject hurtUI = null;
            if (uicgGameComponent.HurtUIPool.Count < 1)
            {
                hurtUI = UnityEngine.Object.Instantiate(uicgGameComponent.HurtUI, uicgGameComponent.HurtUIs.transform, true);
            }
            else
            {
                hurtUI = uicgGameComponent.HurtUIPool[0];
                uicgGameComponent.HurtUIPool.RemoveAt(0);
            }
            hurtUI.SetActive(true);
            hurtUI.transform.position = unitInfo.CardGo.transform.position;
            hurtUI.GetComponentInChildren<Text>().text = $"-{damage}";
            DOTween.Sequence()
                    .AppendInterval(0.6f)
                    .AppendCallback(() => {
                        hurtUI.SetActive(false);
                        uicgGameComponent.HurtUIPool.Add(hurtUI);
                    });
        }

        public static async ETTask<UIUnitInfo> GetUIUnit(this RoomCardInfo cardInfo, UICGGameComponent uicgGameComponent, Room room, GameObject unit, bool isMy)
        {
            ReferenceCollector rc = unit.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(unit);
            unitInfo.CardGo = unit;
            unitInfo.CardId = cardInfo.CardId;
            //Log.Warning($"创建单位:{unitInfo.CardId}");
            unitInfo.BaseId = cardInfo.BaseId;
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            unitInfo.Taunt = rc.Get<GameObject>("Taunt");
            if (isMy)
            {
                //Log.Warning($"单位被添加:{unitInfo.CardId}");
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
                unitInfo.TargetScale = b ? 1.07f : 1f;
            };
            dragHandler.CanBeUsed = () => {
                if (unitInfo.DAttack < 1) {
                    return false;
                }

                if (!unitInfo.AttackCountEnough) {
                    return false;
                }

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
            dragHandler.TryToDoInClient = () =>
            {
                if (unitInfo.TargetInfo != null) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_Attack() { Actor = unitInfo.CardId, Target = unitInfo.TargetInfo.CardId });
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

        public static async ETTask Room2C_FindCardsToShow(Room room, List<RoomCardInfo> cardInfos) {
            Log.Warning("发现展示");
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            await uicgGameComponent.ShowUISelectCard(room, cardInfos);
            uicgGameComponent.UISelect.SetActive(true);
            /*UIUnSelectHandler handler = uicgGameComponent.UISelect.GetComponent<UIUnSelectHandler>();
            handler.Canel = () => {
                Log.Warning($"取消选择");
                uicgGameComponent.UISelect.SetActive(false);
                room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_SelectCard() { CardId = 0 });
            };*/
        }

        public static async ETTask ShowUISelectCard(this UICGGameComponent ui, Room room, List<RoomCardInfo> cardInfos) {
            for (int i = 0; i < 3; i++) {
                if (i >= cardInfos.Count) {
                    ui.UISelectInfos[i].CardGo.SetActive(false);
                } else {
                    ui.UISelectInfos[i].CardGo.SetActive(true);
                    ui.UISelectInfos[i].Room = room;
                    ui.UISelectInfos[i].CardId = cardInfos[i].CardId;
                    ui.UISelectInfos[i].BaseId = cardInfos[i].BaseId;
                    Log.Warning(ui.UISelectInfos[i].CardId);
                    if (cardInfos[i].CardType == (int)CardType.Unit) {
                        ui.UISelectInfos[i].Attack.transform.parent.gameObject.SetActive(true);
                        ui.UISelectInfos[i].HP.transform.parent.gameObject.SetActive(true);
                        ui.UISelectInfos[i].Attack.text = cardInfos[i].Attack.ToString();
                        ui.UISelectInfos[i].HP.text = cardInfos[i].HP.ToString();
                    } else {
                        ui.UISelectInfos[i].Attack.transform.parent.gameObject.SetActive(false);
                        ui.UISelectInfos[i].HP.transform.parent.gameObject.SetActive(false);
                    }

                    ui.UISelectInfos[i].Name.text = CardConfigCategory.Instance.Get(cardInfos[i].BaseId).Name;
                    ui.UISelectInfos[i].Info.text = CardConfigCategory.Instance.Get(cardInfos[i].BaseId).Desc;
                    ui.UISelectInfos[i].Cost.text = cardInfos[i].Cost.ToString();
                    ui.UISelectInfos[i].Red.text = cardInfos[i].Red.ToString();
                    ui.UISelectInfos[i].Black.text = cardInfos[i].Black.ToString();
                    ui.UISelectInfos[i].White.text = cardInfos[i].White.ToString();
                    ui.UISelectInfos[i].Green.text = cardInfos[i].Green.ToString();
                    ui.UISelectInfos[i].Grey.text = cardInfos[i].Grey.ToString();
                    ui.UISelectInfos[i].Blue.text = cardInfos[i].Blue.ToString();
                    string spritePath = $"Assets/Bundles/CardImage/{cardInfos[i].BaseId}.png";
                    Sprite sprite = await room.GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath);
                    ui.UISelectInfos[i].Image.sprite = sprite;
                }
            }
        }

        public static async ETTask<UIUnitInfo> CreateUIHandCard(this UICGGameComponent ui, RoomCardInfo cardInfo, Room room, GameObject card)
        {
            ReferenceCollector rc = card.GetComponent<ReferenceCollector>();
            UIAnimComponent animComponent = ui.GetComponent<UIAnimComponent>();
            UIUnitInfo unitInfo = animComponent.AddChild<UIUnitInfo, GameObject>(card);
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
            unitInfo.CardGo.transform.position = ui.MyGroup.transform.position;

            if (unitInfo.CardType != CardType.Unit) {
                unitInfo.Attack.transform.parent.gameObject.SetActive(false);
                unitInfo.HP.transform.parent.gameObject.SetActive(false);
            }
            
            //抽卡动作
            unitInfo.CardGo.transform.rotation = new Quaternion(0, 90, 90, 0);
            unitInfo.CardGo.SetActive(false);
            unitInfo.IsMove = true;
            animComponent.AppendCallback(() => {
                ui.IsGetHandCardAnim = true;
                unitInfo.CardGo.SetActive(true);
            });
            animComponent.AppendCallback(() => {
                unitInfo.CardGo.transform.DORotate(new Vector3(0, 0, 0), UIAnimComponent.GetCardTime);
            });
            animComponent.Append(unitInfo.CardGo.transform.DOMove(ui.GetHandCardShowPos.transform.position, UIAnimComponent.GetCardTime));
            animComponent.AppendInterval(UIAnimComponent.ShowHandCardTime);
            animComponent.AppendCallback(() => {
                ui.IsGetHandCardAnim = false;
                unitInfo.IsMove = false;
                ui.MyHandCards.Add(unitInfo);
                ui.HandCardsPos();
            });
            
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
                } else if (unitInfo.CardType == CardType.Unit) {
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
            dragHandler.SetUIUnitShow = () => {
                ui.ShowUIUnitShowInfo(unitInfo);
            };
            dragHandler.IsUnitInDrag = () => unitInfo.CardType == CardType.Unit;
            dragHandler.GetHeroVector = () => ui.MyHero.transform.position;
            dragHandler.GetTargetPos = () => unitInfo.TargetPos;
            dragHandler.IsGetHandCardAnim = () => ui.IsGetHandCardAnim;
            dragHandler.CanBeUsed = () =>
            {
                //发牌阶段
                if (ui.IsGetHandCardAnim) {
                    return false;
                }
                //费用不足，无法使用
                if (unitInfo.DCost > ui.DMyCost) {
                    ui.DoTalkUI(true, TalkType.CostNotEnough);
                    return false;
                }
                //场上单位已满
                if (unitInfo.CardType == CardType.Unit && ui.MyFightUnits.Count >= CardGameMsg.UnitMax) {
                    ui.DoTalkUI(true, TalkType.CantHaveMoreUnit);
                    return false;
                }
                return true;
            };
            dragHandler.IsToTargetInDrag = v => {
                if (unitInfo.UseCardType == UseCardType.NoTarget ||
                    unitInfo.CardType == CardType.Unit) {
                    unitInfo.TargetInfo = null;
                    return false;
                } else if (unitInfo.UseCardType == UseCardType.ToActor) {
                    GameObject target = ui.GetActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToUnit) {
                    GameObject target = ui.GetUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyUnit) {
                    GameObject target = ui.GetEnemyUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToMyUnit) {
                    GameObject target = ui.GetMyUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyAgent) {
                    GameObject target = ui.GetEnemyAgentTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToMyAgent) {
                    GameObject target = ui.GetMyAgentTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToMyActor) {
                    GameObject target = ui.GetMyActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyActor) {
                    GameObject target = ui.GetEnemyActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                    return true;
                }
                Log.Error("有没考虑到的情况");
                return false;
            };
            dragHandler.SetTarget = v => {
                if (unitInfo.UseCardType == UseCardType.ToActor) {
                    GameObject target = ui.GetActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToUnit) {
                    GameObject target = ui.GetUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyUnit) {
                    GameObject target = ui.GetEnemyUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToMyUnit) {
                    GameObject target = ui.GetMyUnitTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyAgent) {
                    GameObject target = ui.GetEnemyAgentTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToMyAgent) {
                    GameObject target = ui.GetMyAgentTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToMyActor) {
                    GameObject target = ui.GetMyActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
                } else if (unitInfo.UseCardType == UseCardType.ToEnemyActor) {
                    GameObject target = ui.GetEnemyActorTarget(v);
                    SetTargetInfo(target, unitInfo, ui);
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
            dragHandler.TryToDoInClient = () => {
                ui.SelectCardPos = -1;
                if (unitInfo.CardType == CardType.Unit && unitInfo.UseCardType == UseCardType.ToActor) {
                    ui.MyHandCardUesd = unitInfo;
                    unitInfo.TargetPos = unitInfo.CardGo.transform.position;
                    return true;
                }
                
                if ((unitInfo.CardType == CardType.Magic || unitInfo.CardType == CardType.Plot) && 
                           (unitInfo.UseCardType == UseCardType.ToUnit || 
                               unitInfo.UseCardType == UseCardType.ToHero ||
                               unitInfo.UseCardType == UseCardType.ToActor)) {
                    if (unitInfo.TargetInfo != null) {
                        room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = unitInfo.TargetInfo.CardId, Pos = ui.MyHandCardPos });
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
            dragHandler.SetUnitTargetToDo = () => {
                if (unitInfo.TargetInfo == null)
                    return false;
                if (unitInfo.UseCardType == UseCardType.ToActor) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_UseCard() { Card = cardInfo.CardId, Target = unitInfo.TargetInfo.CardId, Pos = ui.MyHandCardPos });
                    return true;
                }
                return false;
            };
            dragHandler.CancelTarget = () => {
                ui.MyHandCardUesd = null;
            };

            return unitInfo;
        }
        
        //选中目标，并显示特效等
        private static void SetTargetInfo(GameObject target, UIUnitInfo unitInfo, UICGGameComponent ui) {
            if (target != null) {
                unitInfo.TargetInfo = ui.GetComponent<UIAnimComponent>().GetUnitInfoByGo(target);
                target.transform.localScale = new Vector3(1.05f, 1.05f);
            } else if (unitInfo.TargetInfo != null) {
                unitInfo.TargetInfo.CardGo.transform.localScale = Vector3.one;
                unitInfo.TargetInfo = null;
            }
        }

        public static async ETTask Room2C_FlashUnits(Entity room, List<RoomCardInfo> cardInfos, bool isMy) {
            
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            List<UIUnitInfo> unitInfos = isMy? uicgGameComponent.MyFightUnits : uicgGameComponent.EnemyFightUnits;
            foreach (var unitInfo in unitInfos) {
                Log.Warning(cardInfos == null);
                RoomCardInfo cardInfo = cardInfos.GetRoomCardInfoById(unitInfo.CardId);
                if (cardInfo == null) continue;
                unitInfo.DAttack = cardInfo.Attack;
                unitInfo.DHP = cardInfo.HP;
                unitInfo.Attack.text = cardInfo.Attack.ToString();
                unitInfo.HP.text = cardInfo.HP.ToString();
            }
        }

        public static async ETTask Room2C_FlashUnit(Entity room, RoomCardInfo cardInfo) {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            List<UIUnitInfo> unitInfos = new List<UIUnitInfo>(uicgGameComponent.MyFightUnits);
            unitInfos.AddRange(uicgGameComponent.EnemyFightUnits);
            UIUnitInfo unitInfo = unitInfos.GetUIUnitInfoById(cardInfo.CardId);
            unitInfo.DAttack = cardInfo.Attack;
            unitInfo.DHP = cardInfo.HP;
            unitInfo.Attack.text = cardInfo.Attack.ToString();
            unitInfo.HP.text = cardInfo.HP.ToString();
        }
        
        public static UIUnitInfo GetUIUnitInfoById(this List<UIUnitInfo> unitInfos, long id) {
            foreach (var unitInfo in unitInfos) {
                if (unitInfo.CardId == id) {
                    return unitInfo;
                }
            }

            return null;
        } 

        public static RoomCardInfo GetRoomCardInfoById(this List<RoomCardInfo> cardInfos, long id) {
            foreach (var cardInfo in cardInfos) {
                if (cardInfo.CardId == id) {
                    return cardInfo;
                }
            }

            return null;
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
            await SetAgentUnitInfo(room, uicgGameComponent, agent1, agentCard1, isMy);
            //Agent2
            await SetAgentUnitInfo(room, uicgGameComponent, agent2, agentCard2, isMy);
            await ETTask.CompletedTask;
        }
        

        public static void CreateSelectInfo(UICGGameComponent uicgGameComponent) {
            for (int i = 0; i < 3; i++) {
                GameObject card = uicgGameComponent.UISelect.transform.GetChild(i).gameObject;
                UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(card);
                ReferenceCollector rc = card.GetComponent<ReferenceCollector>();
                unitInfo.CardGo = card;
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
                card.GetComponent<UISelectHandler>().ToDo = () => {
                    //Log.Warning($"选择发现选项{unitInfo.CardId}");
                    uicgGameComponent.UISelect.SetActive(false);
                    unitInfo.Room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_SelectCard() { CardId = unitInfo.CardId });
                };
                uicgGameComponent.UISelectInfos.Add(unitInfo);
            }
        }

        private static async ETTask SetAgentUnitInfo(Entity room, UICGGameComponent uicgGameComponent, GameObject agent, RoomCardInfo agentCard, bool isMy) {
            ReferenceCollector rc = agent.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(agent);
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo);
            unitInfo.Attack.text = agentCard.Attack.ToString();
            unitInfo.DAttack = agentCard.Attack;
            unitInfo.DHP = agentCard.HP;
            unitInfo.HP.text = agentCard.HP.ToString();
            unitInfo.CardId = agentCard.CardId;
            unitInfo.BaseId = agentCard.BaseId;
            string spritePath2 = $"Assets/Bundles/CardImage/{agentCard.BaseId}.png";
            try
            {
                Sprite sprite = await (room as Room).GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath2);
                unitInfo.Image.sprite = sprite;
            }
            catch
            {
                Log.Warning($"{spritePath2}还未引入匹配图片");
            }
            UIAgentDragHandler dragHandler = agent.GetComponent<UIAgentDragHandler>();
            dragHandler.IsMy = isMy;
            dragHandler.IsDrag = (b) => {
                unitInfo.IsDrag = b;
                unitInfo.TargetScale = b ? 1.07f : 1f;
            };
            dragHandler.CanBeUsed = () => {
                if (unitInfo.DAttack < 1) {
                    return false;
                }

                if (!unitInfo.AttackCountEnough) {
                    return false;
                }

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
            dragHandler.TryToDoInClient = () =>
            {
                if (unitInfo.TargetInfo != null) {
                    room.Root().GetComponent<ClientSenderCompnent>().Send(new C2Room_Attack() { Actor = unitInfo.CardId, Target = unitInfo.TargetInfo.CardId });
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