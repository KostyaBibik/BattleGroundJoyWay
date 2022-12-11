using Db.Enums;
using UnityEngine;

namespace Db.Actions
{
    public interface IActionBase
    {
        public EActionType ActionType { get; }
        public Sprite ActionSprite { get; }
        public ETargetAction TargetAction { get; }
    }
}