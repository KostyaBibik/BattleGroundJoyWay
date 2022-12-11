using Db.Actions;
using Db.Actions.Impl;
using Db.Enums;
using PlayableItems;

namespace Game.Impl
{
    public class AttackActionState : IActionState
    {
        private CardView _cardView;
        private int _damage;
        private ETargetAction _targetAction;
        public ETargetAction TargetAction => _targetAction;
        
        public IActionState Initialize(
            CardView cardView,
            IActionBase actionBase
            )
        {
            _cardView = cardView;
            var state = (AttackActionVo)actionBase;
            _damage = state.damageValue;
            _cardView.ActionIcon.sprite = state.ActionSprite;
            _targetAction = state.TargetAction;
            
            return this;
        }

        public void OnEndDrag(CardView targetCard)
        {
            targetCard.HealthComponent.ApplyDamage(_damage);
        }
    }
}