using Db.Actions;
using Db.Actions.Impl;
using Db.Enums;
using PlayableItems;

namespace Game.Impl
{
    public class HealActionState : IActionState
    {
        private CardView _cardView;
        private int _healValue;
        private ETargetAction _targetAction;
        public ETargetAction TargetAction => _targetAction;
        
        public IActionState Initialize(
            CardView cardView,
            IActionBase actionBase
            )
        {
            var state = (HealActionVo)actionBase;
            _cardView = cardView;
            _healValue = state.healValue;
            _cardView.ActionIcon.sprite = state.ActionSprite;
            _targetAction = state.TargetAction;

            return this;
        }

        public void OnEndDrag(CardView targetCard)
        {
            targetCard.HealthComponent.AddHealth(_healValue);
        }
    }
}