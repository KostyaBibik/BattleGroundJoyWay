using System;
using System.Collections.Generic;
using Db.Actions;
using Db.Actions.Impl;
using Db.Enums;
using PlayableItems;

namespace Game.Impl
{
    public class DefenceActionState : IActionState
    {
        private CardView _cardView;
        private CardView _targetCard;
        private int _temporaryHealth;
        private int _stepsToRefreshTemporaryHealth;
        private List<Tuple<CardView, int>> defencedCard = new List<Tuple<CardView, int>>();
        private ETargetAction _targetAction;
        public ETargetAction TargetAction  => _targetAction;
        
        public IActionState Initialize(
            CardView cardView, 
            IActionBase actionBase
            )
        {
            _cardView = cardView;
            var state = (DefenceActionVo)actionBase;
            _temporaryHealth = state.temporaryHealthValue;
            _stepsToRefreshTemporaryHealth = state.stepsToRefreshTemporaryHealth;
            _cardView.onEndMove += OnEndTurn;
            _cardView.ActionIcon.sprite = state.ActionSprite;
            _targetAction = state.TargetAction;
            
            return this;
        }

        public void OnEndDrag(CardView targetCard)
        {
            targetCard.HealthComponent.SetTemporaryHealth(_temporaryHealth);
            defencedCard.Add(new Tuple<CardView, int>(targetCard, _stepsToRefreshTemporaryHealth * 2));
        }

        private void OnEndTurn()
        {
            for (var i = 0; i < defencedCard.Count; i++)
            {
                if (defencedCard[i].Item2 - 1 > 0)
                {
                    defencedCard[i] = new Tuple<CardView, int>(defencedCard[i].Item1, defencedCard[i].Item2 - 1);   
                }
                else
                {
                    defencedCard[i].Item1.HealthComponent.SetTemporaryHealth(0);
                    defencedCard.Remove(defencedCard[i]);
                    i--;
                }
            }
        }
    }
}