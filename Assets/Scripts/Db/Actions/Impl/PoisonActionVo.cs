using System;
using Db.Enums;
using UnityEngine;

namespace Db.Actions.Impl
{
    [Serializable]
    public struct PoisonActionVo : IActionBase
    {
        [SerializeField] private EActionType actionType;
        [SerializeField] private ETargetAction targetAction;
        [SerializeField] private Sprite actionSprite;
        
        public int damagePerMotion;
        public int turnsToRemovePoison;
        
        public EActionType ActionType => actionType;
        public Sprite ActionSprite => actionSprite;
        public ETargetAction TargetAction => targetAction;
    }
}