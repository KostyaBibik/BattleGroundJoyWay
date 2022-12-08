using Db.Impl;
using UnityEngine;
using Zenject;

namespace PlayableItems.Logic.Impl
{
    public class CardFactory : ICardFactory
    {
        private readonly DiContainer _diContainer;
        private readonly GameObject _defaultCard;
        
        public CardFactory(
            DiContainer diContainer,
            CardSettings cardSettings
            )
        {
            _diContainer = diContainer;
            _defaultCard = cardSettings.DefaultCard;
        } 
    
        public CardView CreateCard(CardVo cardVo)
        {
            var cardView = _diContainer.InstantiatePrefab(_defaultCard).GetComponent<CardView>();
            cardView.SetHealth(cardVo.health);
            cardView.SetName(cardVo.name);
            
            return cardView;
        }
    }
}