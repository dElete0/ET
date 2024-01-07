using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace ET.Client
{
    [EntitySystemOf(typeof(UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
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
            self.MyHero = rc.Get<GameObject>("MyHero");
            self.EnemyHero = rc.Get<GameObject>("EnemyHero");
            self.MyAgent1 = rc.Get<GameObject>("MyAgent1");
            self.MyAgent2 = rc.Get<GameObject>("MyAgent2");
            self.EnemyAgent1 = rc.Get<GameObject>("EnemyAgent1");
            self.EnemyAgent2 = rc.Get<GameObject>("EnemyAgent2");
            
            // CardDeck
            self.MyHandCardsDeck = rc.Get<GameObject>("MyHandCardsDeck");
            self.EnemyHandCardsDeck = rc.Get<GameObject>("EnemyHandCardsDeck");
            self.MyUnits = rc.Get<GameObject>("MyUnits");
            self.EnemyUnits = rc.Get<GameObject>("EnemyUnits");
            
            // ShowCard
            GameObject uiShowCard = rc.Get<GameObject>("UIShowCard");
            ReferenceCollector uiShowCardRC = uiShowCard.GetComponent<ReferenceCollector>();
            self.UIShowCardInfo = new UIUnitInfo() {
                CardGo = uiShowCard, 
                Attack = uiShowCardRC.Get<GameObject>("Attack").GetComponentInChildren<Text>(),
                HP = uiShowCardRC.Get<GameObject>("HP").GetComponentInChildren<Text>(),
                Image = uiShowCardRC.Get<GameObject>("Image").GetComponent<Image>(),
                Cost = uiShowCardRC.Get<GameObject>("Cost").GetComponent<Text>(),
                Info = uiShowCardRC.Get<GameObject>("Info").GetComponent<Text>(),
                Name = uiShowCardRC.Get<GameObject>("Name").GetComponent<Text>(),
                // Color
                Red = uiShowCardRC.Get<GameObject>("Red").GetComponentInChildren<Text>(),
                Blue = uiShowCardRC.Get<GameObject>("Blue").GetComponentInChildren<Text>(),
                Green = uiShowCardRC.Get<GameObject>("Green").GetComponentInChildren<Text>(),
                White = uiShowCardRC.Get<GameObject>("White").GetComponentInChildren<Text>(),
                Grey = uiShowCardRC.Get<GameObject>("Grey").GetComponentInChildren<Text>(),
                Black = uiShowCardRC.Get<GameObject>("Black").GetComponentInChildren<Text>(),
            };
            
            // Model
            self.UICard = rc.Get<GameObject>("UICard");
            self.UIUnit = rc.Get<GameObject>("UIUnit");
            self.UIEnemyHandCard = rc.Get<GameObject>("UIEnemyHandCard");
            
            // SetActive
            self.TurnStart.SetActive(false);
            self.UICard.SetActive(false);
            self.UIUnit.SetActive(false);
            
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
        private static void Update(this UICGGameComponent self) {
            // 回合开始逻辑
            self.TurnStart();
            // 手牌位置逻辑
            self.HandCardsPos();
            self.EnemyHandCardsPos();
        }

        private static void TurnStart(this UICGGameComponent self) {
            if (self.IsShowTurnStart) {
                self.IsShowTurnStart = false;
                self.IsWaitCloseTurnStart = true;
                self.ShowTurnStartTime = TimeInfo.Instance.ClientNow();
            }

            if (self.IsWaitCloseTurnStart && 
                TimeInfo.Instance.ClientNow() - self.ShowTurnStartTime > UICGGameComponent.ShowTunStartTimeD) {
                self.IsWaitCloseTurnStart = false;
                self.TurnStart.SetActive(false);
            }
        }

        public static void ShowUIShowCard(this UICGGameComponent self, bool left, int baseId) {
            self.UIShowCardInfo.CardGo.SetActive(true);
            var position = self.UIShowCardInfo.CardGo.transform.position;
            if (left) {
                position = new Vector2(
                    -Mathf.Abs(position.x),
                    position.y);
            } else {
                position = new Vector2(
                    Mathf.Abs(position.x),
                    position.y);
            }
            self.UIShowCardInfo.CardGo.transform.position = position;
            
            CardConfig config = CardConfigCategory.Instance.Get(baseId);
            self.UIShowCardInfo.Attack.text = config.Attack.ToString();
            self.UIShowCardInfo.HP.text = config.HP.ToString();
            self.UIShowCardInfo.Info.text = config.Desc;
            self.UIShowCardInfo.Name.text = config.Name;
            self.UIShowCardInfo.Cost.text = config.Cost.ToString();
            self.UIShowCardInfo.Red.text = config.Red.ToString();
            self.UIShowCardInfo.White.text = config.White.ToString();
            self.UIShowCardInfo.Blue.text = config.Blue.ToString();
            self.UIShowCardInfo.Grey.text = config.Grey.ToString();
            self.UIShowCardInfo.Green.text = config.Green.ToString();
            self.UIShowCardInfo.Black.text = config.Black.ToString();
        }

        public static void HideUIShowCard(this UICGGameComponent self) {
            self.UIShowCardInfo.CardGo.SetActive(false);
        }

        private static void C2Room_TurnOver(this UICGGameComponent self) {
            UICGGameHelper.C2Room_TurnOver(self.Root()).Coroutine();
        }

        private static void HandCardsPos(this UICGGameComponent self) {
            if (UIHandCardDragHandler.IsCardBeDrag.Count > 0) return;
            if (self.MyHandCards != null) {
                int i = 0;
                foreach (var card in self.MyHandCards) {
                    i++;
                    card.Value.CardGo.transform.position = (i * 20  - 10 * self.MyHandCards.Count) * Vector3.right + self.MyHandCardsDeck.transform.position;
                }
            }
        }

        private static void EnemyHandCardsPos(this UICGGameComponent self) {
            if (self.EnemyHandCards != null) {
                int i = 0;
                foreach (var card in self.EnemyHandCards) {
                    i++;
                    card.Value.CardGo.transform.position = (i * 20  - 10 * self.EnemyHandCards.Count) * Vector3.right + self.EnemyHandCardsDeck.transform.position;
                }
            }
        }

        public static GameObject GetAttackTarget(this UICGGameComponent self, Vector2 vector2) {
            GameObject target = null;
            target = self.GetEnemyHero(vector2);
            if (target != null) return target;
            target = self.GetEnemyAgent(vector2);
            if (target != null) return target;
            target = self.GetEnemyUnit(vector2);
            if (target != null) return target;
            return null;
        }

        private static GameObject GetEnemyHero(this UICGGameComponent self, Vector2 vector2) {
            if (Mathf.Abs(vector2.x - self.EnemyHero.transform.position.x) < 10 &&
                Mathf.Abs(vector2.y - self.EnemyHero.transform.position.y) < 10) {
                return self.EnemyHero;
            }
            return null;
        }

        private static GameObject GetEnemyAgent(this UICGGameComponent self, Vector2 vector2) {
            if (Mathf.Abs(vector2.x - self.EnemyAgent1.transform.position.x) < 10 &&
                Mathf.Abs(vector2.y - self.EnemyAgent1.transform.position.y) < 10) {
                return self.EnemyAgent1;
            }
            if (Mathf.Abs(vector2.x - self.EnemyAgent2.transform.position.x) < 10 &&
                Mathf.Abs(vector2.y - self.EnemyAgent2.transform.position.y) < 10) {
                return self.EnemyAgent2;
            }

            return null;
        }

        private static GameObject GetEnemyUnit(this UICGGameComponent self, Vector2 vector2) {
            
            foreach (var unit in self.EnemyFightUnits) {
                if (Mathf.Abs(vector2.x - unit.Value.CardGo.transform.position.x) < 20 &&
                    Mathf.Abs(vector2.y - unit.Value.CardGo.transform.position.y) < 20) {
                    return self.EnemyHero;
                }
            }

            return null;
        }
    } 
}