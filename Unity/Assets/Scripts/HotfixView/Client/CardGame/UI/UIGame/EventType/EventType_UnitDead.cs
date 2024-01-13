using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.CardGame)]
    [FriendOfAttribute(typeof(ET.Client.UICGGameComponent))]
    [FriendOfAttribute(typeof(ET.Client.UIUnitInfo))]
    public class EventType_UnitDead : AEvent<Scene, UnitDead>
    {
        protected override async ETTask Run(Scene scene, UnitDead args)
        {
            await UnitDead(scene.GetComponent<Room>(), args.CardIds);
        }

        private static async ETTask UnitDead(Entity room, List<long> cardIds)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            Log.Warning("执行单位死亡动画");
            foreach (var unit in uicgGameComponent.MyFightUnits)
            {
                if (cardIds.Contains(unit.CardId))
                {
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(() => {
                        if (unit.Sequence != null) unit.Sequence.Kill();
                        unit.CardGo.transform.eulerAngles = new Vector3(10, 10, 10);
                        unit.Sequence = DOTween.Sequence().InsertCallback(1.5f, () => {
                            Log.Warning($"单位被移除:{unit.CardId}");
                            uicgGameComponent.MyFightUnits.Remove(unit);
                            uicgGameComponent.UnitPool.Add(unit.CardGo);
                            unit.CardGo.SetActive(false);
                        });
                    });
                }
            }
            foreach (var unit in uicgGameComponent.EnemyFightUnits)
            {
                if (cardIds.Contains(unit.CardId))
                {
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(() => {
                        if (unit.Sequence != null) unit.Sequence.Kill();
                        unit.CardGo.transform.eulerAngles = new Vector3(10, 10, 10);
                        unit.Sequence = DOTween.Sequence().InsertCallback(1.5f, () => {
                            uicgGameComponent.EnemyFightUnits.Remove(unit);
                            uicgGameComponent.UnitPool.Add(unit.CardGo);
                            unit.CardGo.SetActive(false);
                        });
                    });
                }
            }
        }
    }
}