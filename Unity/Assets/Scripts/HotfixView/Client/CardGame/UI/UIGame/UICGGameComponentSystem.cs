using System.IO;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public static partial class UICGGameComponentSystem
    {
        [EntitySystem]
        private static void Awake(this UICGGameComponent self) {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.TurnStart = rc.Get<GameObject>("TurnStart");
            self.TurnOver = rc.Get<GameObject>("TurnOver");
            self.EnemyAccount = rc.Get<GameObject>("EnemyAccount").GetComponentInChildren<Text>();

            self.TurnOver.GetComponent<Button>().onClick.AddListener(() => { self.C2Room_TurnOver(); });

            // Cost
            self.MyCost = rc.Get<GameObject>("MyCost").GetComponentInChildren<Text>();
            self.EnemyCost = rc.Get<GameObject>("EnemyCost").GetComponentInChildren<Text>();

            self.MyCost.text = "0/0";
            self.EnemyCost.text = "0/0";

            self.MyRed = rc.Get<GameObject>("MyRed").GetComponentInChildren<Text>();
            self.MyBlue = rc.Get<GameObject>("MyBlue").GetComponentInChildren<Text>();
            self.MyWhite = rc.Get<GameObject>("MyWhite").GetComponentInChildren<Text>();
            self.MyGreen = rc.Get<GameObject>("MyGreen").GetComponentInChildren<Text>();
            self.MyBlack = rc.Get<GameObject>("MyBlack").GetComponentInChildren<Text>();
            self.MyGrey = rc.Get<GameObject>("MyGrey").GetComponentInChildren<Text>();

            self.EnemyRed = rc.Get<GameObject>("EnemyRed").GetComponentInChildren<Text>();
            self.EnemyBlue = rc.Get<GameObject>("EnemyBlue").GetComponentInChildren<Text>();
            self.EnemyWhite = rc.Get<GameObject>("EnemyWhite").GetComponentInChildren<Text>();
            self.EnemyGreen = rc.Get<GameObject>("EnemyGreen").GetComponentInChildren<Text>();
            self.EnemyBlack = rc.Get<GameObject>("EnemyBlack").GetComponentInChildren<Text>();
            self.EnemyGrey = rc.Get<GameObject>("EnemyGrey").GetComponentInChildren<Text>();

            // HeroDeck
            self.MyTalkUI = rc.Get<GameObject>("MyTalkUI").GetComponentInChildren<Text>();
            self.EnemyTalkUI = rc.Get<GameObject>("EnemyTalkUI").GetComponentInChildren<Text>();
            self.MyTalkUI.transform.parent.gameObject.SetActive(false);
            self.EnemyTalkUI.transform.parent.gameObject.SetActive(false);

            // CardDeck
            self.MyHandCardsDeck = rc.Get<GameObject>("MyHandCardsDeck");
            self.EnemyHandCardsDeck = rc.Get<GameObject>("EnemyHandCardsDeck");
            self.MyUnits = rc.Get<GameObject>("MyUnits");
            self.EnemyUnits = rc.Get<GameObject>("EnemyUnits");
            self.HurtUIs = rc.Get<GameObject>("HurtUIs");

            // Group
            self.MyGroup = rc.Get<GameObject>("MyGroup");
            self.EnemyGroup = rc.Get<GameObject>("EnemyGroup");
            self.GetHandCardShowPos = rc.Get<GameObject>("GetHandCardShowPos");
            
            // Select
            self.UISelect = rc.Get<GameObject>("UISelect");
            self.UISelect.SetActive(false);

            // Model
            self.UICard = rc.Get<GameObject>("UICard");
            self.UIUnit = rc.Get<GameObject>("UIUnit");
            self.HurtUI = rc.Get<GameObject>("HurtUI");
            self.UIEnemyHandCard = rc.Get<GameObject>("UIEnemyHandCard");

            // SetActive
            self.TurnStart.SetActive(false);
            self.UICard.SetActive(false);
            self.UIUnit.SetActive(false);
            self.HurtUI.SetActive(false);
            self.UIEnemyHandCard.SetActive(false);

            // MyColor
            self.MyRed.transform.parent.gameObject.SetActive(false);
            self.MyBlue.transform.parent.gameObject.SetActive(false);
            self.MyWhite.transform.parent.gameObject.SetActive(false);
            self.MyGreen.transform.parent.gameObject.SetActive(false);
            self.MyBlack.transform.parent.gameObject.SetActive(false);
            self.MyGrey.transform.parent.gameObject.SetActive(false);

            // EnemyColor
            self.EnemyRed.transform.parent.gameObject.SetActive(false);
            self.EnemyBlue.transform.parent.gameObject.SetActive(false);
            self.EnemyWhite.transform.parent.gameObject.SetActive(false);
            self.EnemyGreen.transform.parent.gameObject.SetActive(false);
            self.EnemyBlack.transform.parent.gameObject.SetActive(false);
            self.EnemyGrey.transform.parent.gameObject.SetActive(false);
        }

        [EntitySystem]
        private static void Update(this UICGGameComponent self)
        {
            // 回合开始逻辑
            self.TurnStart();
            // 手牌位置逻辑
            self.HandCardsPos();
            self.EnemyHandCardsPos();
            self.MyUnitsPos();
            self.EnemyUnitsPos();
        }

        private static void TurnStart(this UICGGameComponent self)
        {
            if (self.IsShowTurnStart)
            {
                self.IsShowTurnStart = false;
                self.IsWaitCloseTurnStart = true;
                self.ShowTurnStartTime = TimeInfo.Instance.ClientNow();
            }

            if (self.IsWaitCloseTurnStart &&
                TimeInfo.Instance.ClientNow() - self.ShowTurnStartTime > UICGGameComponent.ShowTunStartTimeD)
            {
                self.IsWaitCloseTurnStart = false;
                self.TurnStart.SetActive(false);
            }
        }
        
        public static void DoTalkUI(this UICGGameComponent self, bool isMy, TalkType talkType) {
            Text talkUI = isMy? self.MyTalkUI : self.EnemyTalkUI;
            Sequence talkSequence = isMy? self.MyTalkSequence : self.EnemyTalkSequence;
            string talk = "";
            float waitTime = 0f;
            switch (talkType) {
                case TalkType.CantDoNow:
                    talk = "这不是我的回合";
                    waitTime = 2f;
                    break;
                case TalkType.CostNotEnough:
                    talk = "我的战备资源不足";
                    waitTime = 2f;
                    break;
                case TalkType.CantHaveMoreUnit:
                    talk = "我无法拥有更多单位";
                    waitTime = 2f;
                    break;
                default:
                    return;
            }
            talkUI.transform.parent.gameObject.SetActive(true);
            talkUI.text = talk;
            if (talkSequence != null) {
                talkSequence.Kill();
            }
            self.EnemyTalkSequence = DOTween.Sequence()
                    .AppendInterval(waitTime)
                    .AppendCallback(() => talkUI.transform.parent.gameObject.SetActive(false));
        }

        public static async ETTask CreateHeroAndAgent(this UICGGameComponent self) {
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            self.MyHero = self.CreateHero(rc.Get<GameObject>("MyHero"));
            self.EnemyHero = self.CreateHero(rc.Get<GameObject>("EnemyHero"));
            self.MyAgent1 = self.CreateAgent(rc.Get<GameObject>("MyAgent1"));
            self.MyAgent2 = self.CreateAgent(rc.Get<GameObject>("MyAgent2"));
            self.EnemyAgent1 = self.CreateAgent(rc.Get<GameObject>("EnemyAgent1"));
            self.EnemyAgent2 = self.CreateAgent(rc.Get<GameObject>("EnemyAgent2"));
            await ETTask.CompletedTask;
        }
        
        private static UIUnitInfo CreateAgent(this UICGGameComponent uicgGameComponent, GameObject agentGo) {
            ReferenceCollector rc = agentGo.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(agentGo);
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo);
            return unitInfo;
        }

        private static UIUnitInfo CreateHero(this UICGGameComponent uicgGameComponent, GameObject hero) {
            ReferenceCollector rc = hero.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = uicgGameComponent.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(hero);
            unitInfo.Attack = rc.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = rc.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Armor = rc.Get<GameObject>("Armor").GetComponentInChildren<Text>();
            unitInfo.Image = rc.Get<GameObject>("Image").GetComponent<Image>();
            uicgGameComponent.HeroAndAgent.Add(unitInfo);
            unitInfo.Attack.transform.parent.gameObject.SetActive(false);
            unitInfo.Armor.transform.parent.gameObject.SetActive(false);
            return unitInfo;
        }

        public static async ETTask CreateUIShowCard(this UICGGameComponent self) {
            // ShowCard
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject uiShowCard = rc.Get<GameObject>("UIShowCard");
            ReferenceCollector uiShowCardRC = uiShowCard.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = self.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(uiShowCard);
            unitInfo.Attack = uiShowCardRC.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = uiShowCardRC.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = uiShowCardRC.Get<GameObject>("Image").GetComponent<Image>();
            unitInfo.Cost = uiShowCardRC.Get<GameObject>("Cost").GetComponentInChildren<Text>();
            unitInfo.Info = uiShowCardRC.Get<GameObject>("Info").GetComponent<Text>();
            unitInfo.Name = uiShowCardRC.Get<GameObject>("Name").GetComponent<Text>();
            // Color
            unitInfo.Red = uiShowCardRC.Get<GameObject>("Red").GetComponentInChildren<Text>();
            unitInfo.Blue = uiShowCardRC.Get<GameObject>("Blue").GetComponentInChildren<Text>();
            unitInfo.Green = uiShowCardRC.Get<GameObject>("Green").GetComponentInChildren<Text>();
            unitInfo.White = uiShowCardRC.Get<GameObject>("White").GetComponentInChildren<Text>();
            unitInfo.Grey = uiShowCardRC.Get<GameObject>("Grey").GetComponentInChildren<Text>();
            unitInfo.Black = uiShowCardRC.Get<GameObject>("Black").GetComponentInChildren<Text>();
            self.UIShowCardInfo = unitInfo;
            uiShowCard.SetActive(false);
            await ETTask.CompletedTask;
        }
        
        public static async ETTask CreateUIUnitShowInfo(this UICGGameComponent self) {
            // ShowCard
            ReferenceCollector rc = self.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            GameObject uiShowCard = rc.Get<GameObject>("UIUnitShow");
            ReferenceCollector uiShowCardRC = uiShowCard.GetComponent<ReferenceCollector>();
            UIUnitInfo unitInfo = self.GetComponent<UIAnimComponent>().AddChild<UIUnitInfo, GameObject>(uiShowCard);
            unitInfo.Attack = uiShowCardRC.Get<GameObject>("Attack").GetComponentInChildren<Text>();
            unitInfo.HP = uiShowCardRC.Get<GameObject>("HP").GetComponentInChildren<Text>();
            unitInfo.Image = uiShowCardRC.Get<GameObject>("Image").GetComponent<Image>();
            unitInfo.TargetPos = new Vector3(-999999, -999999);
            unitInfo.CardGo.transform.position = new Vector3(-999999, -999999);
            UIUnitShowHandler.IsBeDrag = (b) => {
                unitInfo.IsDrag = b;
            };
            self.UIUnitShowInfo = unitInfo;
            await ETTask.CompletedTask;
        }

        public static void ShowUIUnitShowInfo(this UICGGameComponent self, UIUnitInfo card) {
            UIUnitInfo unitShow = self.UIUnitShowInfo;
            unitShow.Attack.text = card.DAttack.ToString();
            unitShow.HP.text = card.DHP.ToString();
            unitShow.Image.sprite = card.Image.sprite;
        }

        public static async ETTask ShowUIShowCard(this UICGGameComponent self, bool left, int baseId)
        {
            self.UIShowCardInfo.CardGo.SetActive(true);
            var position = self.UIShowCardInfo.CardGo.transform.localPosition;
            if (left)
            {
                self.UIShowCardInfo.CardGo.transform.localPosition = new Vector3(
                    -Mathf.Abs(position.x),
                    position.y, 0);
            }
            else
            {
                self.UIShowCardInfo.CardGo.transform.localPosition = new Vector3(
                    Mathf.Abs(position.x),
                    position.y, 0);
            }

            CardConfig config = CardConfigCategory.Instance.Get(baseId);
            self.UIShowCardInfo.Attack.text = config.Attack.ToString();
            self.UIShowCardInfo.HP.text = config.HP.ToString();
            self.UIShowCardInfo.Info.text = config.Desc;
            self.UIShowCardInfo.Name.text = config.Name;
            self.UIShowCardInfo.Cost.text = config.Cost.ToString();
            //Sprite
            string spritePath = $"Assets/Bundles/CardImage/{baseId}.png";
            UIComponent uiComponent = self.GetParent<UIComponent>();
            try
            {
                Sprite sprite = await uiComponent.Room().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<Sprite>(spritePath);
                self.UIShowCardInfo.Image.sprite = sprite;
            }
            catch
            {
                Log.Warning($"{spritePath}还未引入匹配图片");
            }
            //Color
            self.UIShowCardInfo.Red.text = config.Red.ToString();
            self.UIShowCardInfo.White.text = config.White.ToString();
            self.UIShowCardInfo.Blue.text = config.Blue.ToString();
            self.UIShowCardInfo.Grey.text = config.Grey.ToString();
            self.UIShowCardInfo.Green.text = config.Green.ToString();
            self.UIShowCardInfo.Black.text = config.Black.ToString();
            self.UIShowCardInfo.Red.transform.parent.gameObject.SetActive(config.Red > 0);
            self.UIShowCardInfo.White.transform.parent.gameObject.SetActive(config.White > 0);
            self.UIShowCardInfo.Blue.transform.parent.gameObject.SetActive(config.Blue > 0);
            self.UIShowCardInfo.Grey.transform.parent.gameObject.SetActive(config.Grey > 0);
            self.UIShowCardInfo.Green.transform.parent.gameObject.SetActive(config.Green > 0);
            self.UIShowCardInfo.Black.transform.parent.gameObject.SetActive(config.Black > 0);
        }

        public static void HideUIShowCard(this UICGGameComponent self)
        {
            self.UIShowCardInfo.CardGo.SetActive(false);
        }

        private static void C2Room_TurnOver(this UICGGameComponent self)
        {
            UICGGameHelper.C2Room_TurnOver(self.Root()).Coroutine();
        }

        public static void HandCardsPos(this UICGGameComponent self)
        {
            if (UIHandCardDragHandler.IsCardBeDrag.Count > 0) return;
            if (self.MyHandCardUesd != null) {
                bool isBehand = false;
                for (int i = 0; i < self.MyHandCards.Count; i++) {
                    if (self.MyHandCardUesd.CardId == self.MyHandCards[i].CardId) {
                        isBehand = true;
                        continue;
                    }
                    self.MyHandCards[i].TargetPos =
                            ((i - (isBehand ? 1 : 0)) * UICGGameComponent.MyHandCardDes -
                                UICGGameComponent.MyHandCardDes / 2 * self.MyHandCards.Count) * Vector3.right +
                            self.MyHandCardsDeck.transform.position;
                    self.MyHandCards[i].CardGo.transform.SetSiblingIndex(i);
                }
            } else {
                if (self.SelectCardPos > -1) {
                    for (int i = 0; i < self.MyHandCards.Count; i++) {
                        self.MyHandCards[i].TargetPos =
                                (i * UICGGameComponent.MyHandCardDes -
                                    UICGGameComponent.MyHandCardDes / 2 * self.MyHandCards.Count) * Vector3.right +
                                self.MyHandCardsDeck.transform.position;
                        if (i < self.SelectCardPos) {
                            self.MyHandCards[i].TargetPos -= UICGGameComponent.MyHandCardDes * 1.9f * Vector3.right;
                        } else if (i > self.SelectCardPos) {
                            self.MyHandCards[i].TargetPos += UICGGameComponent.MyHandCardDes * 1.9f * Vector3.right;
                            //self.MyHandCards[i].CardGo.transform.SetSiblingIndex(i - 1);
                        } else {
                            self.MyHandCards[i].TargetPos += UICGGameComponent.MyHandCardDes * 1.9f * Vector3.up;
                            //self.MyHandCards[i].CardGo.transform.SetSiblingIndex(self.MyHandCards.Count);
                        }
                        self.MyHandCards[i].CardGo.transform.SetSiblingIndex(i);
                    }
                } else {
                    for (int i = 0; i < self.MyHandCards.Count; i++) {
                        self.MyHandCards[i].TargetPos =
                                (i * UICGGameComponent.MyHandCardDes -
                                    UICGGameComponent.MyHandCardDes / 2 * self.MyHandCards.Count) * Vector3.right +
                                self.MyHandCardsDeck.transform.position;
                        self.MyHandCards[i].CardGo.transform.SetSiblingIndex(i);
                    }
                }
            }
        }

        public static void EnemyHandCardsPos(this UICGGameComponent self)
        {
            if (self.EnemyHandCards != null)
            {
                int i = 0;
                foreach (var card in self.EnemyHandCards)
                {
                    card.CardGo.transform.SetSiblingIndex(i);
                    card.TargetPos = (i * UICGGameComponent.EnemyHandCardDes - UICGGameComponent.EnemyHandCardDes / 2 * self.EnemyHandCards.Count) * Vector3.right + self.EnemyHandCardsDeck.transform.position;
                    i++;
                }
            }
        }

        private static void MyUnitsPos(this UICGGameComponent self)
        {
            if (self.MyFightUnits != null)
            {
                for (int i = self.MyFightUnits.Count - 1; i > 0; --i) {
                    if (self.MyFightUnits[i] == null) {
                        self.MyFightUnits.RemoveAt(i);
                    }
                }
                if (self.MyHandCardPos == -1)
                {
                    
                    for (int i = 0; i < self.MyFightUnits.Count; i++) {
                        self.MyFightUnits[i].TargetPos =
                                (i * UICGGameComponent.UnitsDes - UICGGameComponent.UnitsDes / 2 * (self.MyFightUnits.Count - 1)) * Vector3.right +
                                self.MyUnits.transform.position;
                    }
                } else {
                    bool isBehand = false;
                    if (self.MyFightUnits.Count == self.MyHandCardPos && self.MyHandCardUesd != null) {
                        self.MyHandCardUesd.TargetPos = (self.MyFightUnits.Count * UICGGameComponent.UnitsDes - UICGGameComponent.UnitsDes / 2 * self.MyFightUnits.Count) * Vector3.right +
                                self.MyUnits.transform.position;
                    }
                    for (int i = 0; i < self.MyFightUnits.Count; i++) {
                        if (i == self.MyHandCardPos) {
                            isBehand = true;
                            if (self.MyHandCardUesd != null) {
                                self.MyHandCardUesd.TargetPos = (i * UICGGameComponent.UnitsDes - UICGGameComponent.UnitsDes / 2 * self.MyFightUnits.Count) * Vector3.right +
                                        self.MyUnits.transform.position;
                            }
                        }
                        self.MyFightUnits[i].TargetPos =
                                ((i + (isBehand ? 1 : 0)) * UICGGameComponent.UnitsDes - UICGGameComponent.UnitsDes / 2 * self.MyFightUnits.Count) * Vector3.right +
                                self.MyUnits.transform.position;
                    }
                }
            }
        }

        private static void EnemyUnitsPos(this UICGGameComponent self)
        {
            for (int i = self.EnemyFightUnits.Count - 1; i > 0; --i) {
                if (self.EnemyFightUnits[i] == null) {
                    self.EnemyFightUnits.RemoveAt(i);
                }
            }
            if (self.EnemyFightUnits != null)
            {
                int i = 0;
                foreach (var card in self.EnemyFightUnits)
                {
                    card.TargetPos =
                            (i * UICGGameComponent.UnitsDes - UICGGameComponent.UnitsDes / 2 * (self.EnemyFightUnits.Count - 1)) * Vector3.right +
                            self.EnemyUnits.transform.position;
                    i++;
                }
            }
        }

        public static UIUnitInfo GetAttackTarget(this UICGGameComponent self, Vector2 vector2)
        {
            UIUnitInfo target = null;
            target = self.GetEnemyHero(vector2);
            if (target != null) return target;
            target = self.GetEnemyAgent(vector2);
            if (target != null) return target;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            return null;
        }

        public static UIUnitInfo GetActorTarget(this UICGGameComponent self, Vector2 vector2)
        {
            UIUnitInfo target = null;
            target = self.GetEnemyHero(vector2);
            if (target != null) return target;
            target = self.GetEnemyAgent(vector2);
            if (target != null) return target;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            target = self.GetMyUnit(vector2);
            if (target != null) return target;
            target = self.GetMyHero(vector2);
            if (target != null) return target;
            target = self.GetMyAgent(vector2);
            if (target != null) return target;
            return null;
        }

        public static UIUnitInfo GetUnitTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            target = self.GetMyUnit(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetEnemyUnitTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetMyUnitTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetMyUnit(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetEnemyAgentTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetEnemyAgent(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetMyAgentTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetMyAgent(vector2);
            if (target != null) return target;
            return null;
        }

        public static UIUnitInfo GetMyActorTarget(this UICGGameComponent self, Vector2 vector2) {
            UIUnitInfo target = null;
            target = self.GetMyUnit(vector2);
            if (target != null) return target;
            target = self.GetMyHero(vector2);
            if (target != null) return target;
            target = self.GetMyAgent(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetEnemyActorTarget(this UICGGameComponent self, Vector2 vector2)
        {
            UIUnitInfo target = null;
            target = self.GetEnemyHero(vector2);
            if (target != null) return target;
            target = self.GetEnemyAgent(vector2);
            if (target != null) return target;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            return null;
        }
        
        public static UIUnitInfo GetUIUnitInfoById(this UICGGameComponent uicgGameComponent, long id) {
            foreach (var cardUnitInfo in uicgGameComponent.HeroAndAgent)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.MyFightUnits)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }
            foreach (var cardUnitInfo in uicgGameComponent.EnemyFightUnits)
            {
                if (id == cardUnitInfo.CardId)
                {
                    return cardUnitInfo;
                }
            }

            return null;
        }

        private static UIUnitInfo GetEnemyHero(this UICGGameComponent self, Vector2 vector2)
        {
            if (Mathf.Abs(vector2.x - self.EnemyHero.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.EnemyHero.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.EnemyHero;
            }
            return null;
        }

        private static UIUnitInfo GetMyHero(this UICGGameComponent self, Vector2 vector2)
        {
            if (Mathf.Abs(vector2.x - self.MyHero.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.MyHero.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.MyHero;
            }
            return null;
        }

        private static UIUnitInfo GetEnemyAgent(this UICGGameComponent self, Vector2 vector2)
        {
            if (Mathf.Abs(vector2.x - self.EnemyAgent1.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.EnemyAgent1.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.EnemyAgent1;
            }
            if (Mathf.Abs(vector2.x - self.EnemyAgent2.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.EnemyAgent2.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.EnemyAgent2;
            }

            return null;
        }

        private static UIUnitInfo GetMyAgent(this UICGGameComponent self, Vector2 vector2)
        {
            if (Mathf.Abs(vector2.x - self.MyAgent1.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.MyAgent1.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.MyAgent1;
            }
            if (Mathf.Abs(vector2.x - self.MyAgent2.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                Mathf.Abs(vector2.y - self.MyAgent2.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
            {
                return self.MyAgent2;
            }

            return null;
        }

        private static UIUnitInfo GetEnemyUnit(this UICGGameComponent self, Vector2 vector2)
        {

            foreach (var unit in self.EnemyFightUnits)
            {
                if (Mathf.Abs(vector2.x - unit.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                    Mathf.Abs(vector2.y - unit.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
                {
                    return unit;
                }
            }

            return null;
        }

        private static UIUnitInfo GetMyUnit(this UICGGameComponent self, Vector2 vector2)
        {

            foreach (var unit in self.MyFightUnits)
            {
                if (Mathf.Abs(vector2.x - unit.CardGo.transform.position.x) < UICGGameComponent.FindTarget &&
                    Mathf.Abs(vector2.y - unit.CardGo.transform.position.y) < UICGGameComponent.FindTarget)
                {
                    return unit;
                }
            }

            return null;
        }
    }
}