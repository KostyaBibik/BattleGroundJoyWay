using System;
using System.Collections.Generic;
using Db.Actions;
using Db.Actions.Impl;
using Db.Enums;
using PlayableItems;

namespace Game.Impl
{
    public class PoisonActionState : IActionState
    {
        private CardView _cardView;
        private int _periodicDamage;
        private List<Tuple<CardView, int>> poisonedCards = new List<Tuple<CardView, int>>();
        private int _turnsToRemovePoison;
        private ETargetAction _targetAction;
        public ETargetAction TargetAction => _targetAction;
        
        public IActionState Initialize(
            CardView cardView,
            IActionBase actionBase
        )
        {
            _cardView = cardView;
            var state = (PoisonActionVo)actionBase;
            _periodicDamage = state.damagePerMotion;
            _turnsToRemovePoison = state.turnsToRemovePoison;
            _targetAction = state.TargetAction;
            
            _cardView.ActionIcon.sprite = state.ActionSprite;
            _cardView.onEndMove += OnEndTurn;
            
            return this;
        }

        public void OnEndDrag(CardView targetCard)
        { 
            targetCard.HealthComponent.ApplyDamage(_periodicDamage);
            targetCard.HealthComponent.SwitchPoisonEffect(true);
            poisonedCards.Add(new Tuple<CardView, int>(targetCard, _turnsToRemovePoison * 2));
        }

        private void OnEndTurn()
        {
            for (var i = 0; i < poisonedCards.Count; i++)
            {
                if (poisonedCards[i].Item2 - 1 > 0 && poisonedCards[i].Item1.HealthComponent.IsPoisoned)
                {
                    poisonedCards[i] = new Tuple<CardView, int>(poisonedCards[i].Item1, poisonedCards[i].Item2 - 1);   
                }
                else
                {
                    if(poisonedCards[i].Item1.HealthComponent.IsPoisoned)
                    {
                        poisonedCards[i].Item1.HealthComponent.ApplyDamage(_periodicDamage);
                        poisonedCards[i].Item1.HealthComponent.SwitchPoisonEffect(false);
                    }

                    poisonedCards.Remove(poisonedCards[i]);
                    i--;
                }
            }
        }
    }
}