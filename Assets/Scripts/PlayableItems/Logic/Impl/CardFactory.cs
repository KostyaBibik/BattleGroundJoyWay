using Db;
using UnityEngine;
using Zenject;

namespace PlayableItems.Logic.Impl
{
    public class CardFactory : ICardFactory
    {
        private readonly DiContainer _diContainer;
        private readonly CardPrefabs _cardPrefabs;
        
        public CardFactory(
            DiContainer diContainer,
            CardPrefabs cardPrefabs
            )
        {
            _diContainer = diContainer;
            _cardPrefabs = cardPrefabs;
        } 
    
        public GameObject CreateCard()
        {
            return _diContainer.InstantiatePrefab(_cardPrefabs.CardView);
        }
    }
}