using Db.Impl;
using UnityEngine;
using Zenject;

namespace PlayableItems.Logic.Impl
{
    public class CardFactory : ICardFactory
    {
        private readonly DiContainer _diContainer;
        private readonly GameObject _defaultCard;
        private readonly SignalBus _signalBus;

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
            var cardView = _diContainer.InstantiatePrefabForComponent<CardView>(_defaultCard).Initialize(cardVo);
            
            return cardView;
        }
    }
}