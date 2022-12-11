using Db.Enums;
using Game;
using PlayableItems;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems
{
    public class CardMovingSystem
    {
        private readonly Camera _camera;
        private readonly IndicatorView _indicatorView;
        
        private CardView _selectedCard;
        private CardView _savedDropCard;

        public bool getSelectedCard;
        
        public CardMovingSystem(
            Camera camera, 
            IndicatorView indicatorView
        )
        {
            _camera = camera;
            _indicatorView = indicatorView;
        }
        
        public void OnBeginDrag(PointerEventData eventData, CardView cardView)
        {
            _selectedCard = cardView;
            var inputPoint = new Vector3(eventData.position.x, eventData.position.y, 0f);
            var inputPos = _camera.ScreenToWorldPoint(inputPoint);
            _indicatorView.SetPoint(0, inputPos);
            getSelectedCard = true;
        }
        
        public void OnDrag(PointerEventData eventData, CardView cardView)
        {
            var inputPoint = new Vector3(eventData.position.x, eventData.position.y, 0f);
            var inputPos = _camera.ScreenToWorldPoint(inputPoint);
            _indicatorView.SetPoint(1, inputPos);
        }
        
        public void OnEndDrag(PointerEventData eventData, CardView cardView)
        {
            if(!_selectedCard)
                return;
            
            if(_savedDropCard)
            {
                _selectedCard.ActionState.OnEndDrag(_savedDropCard);
            }
            else
            {
                cardView.SwitchActiveState(true);
            }

            getSelectedCard = false;
            _indicatorView.RefreshIndicator();
            _savedDropCard = null;
            _selectedCard = null;
        }
        
        public void OnDrop(CardView dropCard)
        {
            if(!_selectedCard)
                return;
            
            var targetTeam = _selectedCard.ActionState.TargetAction;

            if (targetTeam is ETargetAction.AllTeams)
            {
                _savedDropCard = dropCard;
                return;
            }

            if (_selectedCard.GetTeam() == ETeam.Player)
            {
                if (dropCard.GetTeam() == ETeam.Enemy && targetTeam == ETargetAction.EnemyTeam)
                {
                    _savedDropCard = dropCard;
                    return;
                }
                
                if (dropCard.GetTeam() == ETeam.Player && targetTeam == ETargetAction.SelfTeam)
                {
                    _savedDropCard = dropCard;
                    return;
                }
            }
            
            if (_selectedCard.GetTeam() == ETeam.Enemy)
            {
                if (dropCard.GetTeam() == ETeam.Player && targetTeam == ETargetAction.EnemyTeam)
                {
                    _savedDropCard = dropCard;
                    return;
                }
                
                if (dropCard.GetTeam() == ETeam.Enemy && targetTeam == ETargetAction.SelfTeam)
                {
                    _savedDropCard = dropCard;
                }
            }
        }
    }
}