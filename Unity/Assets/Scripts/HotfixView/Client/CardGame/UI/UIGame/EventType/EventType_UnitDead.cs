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
            Log.Warning($"客户端:执行{cardIds.Count}个单位死亡");
            foreach (var unit in uicgGameComponent.MyFightUnits)
            {
                if (cardIds.Contains(unit.CardId))
                {
                    //Log.Warning($"客户端:执行单位死亡动画{unit.Name}");
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(() => {
                        //if (unit.Sequence != null) unit.Sequence.Kill();
                        unit.CardGo.transform.eulerAngles = new Vector3(10, 10, 10);
                        unit.Sequence = DOTween.Sequence().InsertCallback(1.5f, () => {
                            //Log.Warning($"客户端:单位被移除:{unit.Name}");
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