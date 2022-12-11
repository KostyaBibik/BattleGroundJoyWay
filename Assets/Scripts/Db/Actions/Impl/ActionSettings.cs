using System;
using UnityEngine;

namespace Db.Actions.Impl
{
    [CreateAssetMenu(menuName = "Settings/" + nameof(ActionSettings),
        fileName = nameof(ActionSettings))]
    public class ActionSettings : ScriptableObject, IActionSettings
    {
        [SerializeField] private AttackActionVo attackActionVo;
        [SerializeField] private HealActionVo healActionVo;
        [SerializeField] private PoisonActionVo poisonActionVo;
        [SerializeField] private DefenceActionVo defenceActionVo;
        
        public IActionBase GetAction(EActionType actionType)
        {
            return actionType switch
            {
                EActionType.Attack => attackActionVo,
                EActionType.Defend => defenceActionVo,
                EActionType.Heal => healActionVo,
                EActionType.Poison => poisonActionVo,
                _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
            };
        }
    }
}