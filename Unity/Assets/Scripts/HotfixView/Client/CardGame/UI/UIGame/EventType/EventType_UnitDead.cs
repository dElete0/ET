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
            await UnitDead(scene.GetComponent<Room>(), args.CardId);
        }

        private static async ETTask UnitDead(Entity room, long cardId)
        {
            UI ui = await UIHelper.Get(room, UIType.UICGGame);
            UICGGameComponent uicgGameComponent = ui.GetComponent<UICGGameComponent>();
            foreach (var unit in uicgGameComponent.MyFightUnits)
            {
                if (unit.CardId == cardId)
                {
                    uicgGameComponent.MyFightUnits.Remove(unit);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.UnitPool.Add(unit.CardGo);
                    uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(() => {
                        unit.Sequence.Kill();
                        unit.CardGo.transform.rotation = new Quaternion(10, 10, 10, 10);
                        unit.Sequence = DOTween.Sequence().InsertCallback(0.7f, () =>
                                unit.CardGo.SetActive(false));
                    });
                    return;
                }
            }
            foreach (var unit in uicgGameComponent.EnemyFightUnits)
            {
                if (unit.CardId == cardId)
                {
                    uicgGameComponent.MyFightUnits.Remove(unit);
                    uicgGameComponent.GetComponent<UIAnimComponent>().RemoveChild(unit.Id);
                    uicgGameComponent.UnitPool.Add(unit.CardGo);
                    uicgGameComponent.GetComponent<UIAnimComponent>().AppendCallback(() => {
                        unit.Sequence.Kill();
                        unit.CardGo.transform.rotation = new Quaternion(10, 10, 10, 10);
                        unit.Sequence = DOTween.Sequence().InsertCallback(0.7f, () =>
                                unit.CardGo.SetActive(false));
                    });
                    return;
                }
            }
        }
    }
}