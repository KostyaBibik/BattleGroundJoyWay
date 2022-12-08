using System;
using Db.Impl;
using PlayableItems.Logic;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace Systems
{
    public class CardInitializeSystem : IInitializable, IDisposable
    {
        private readonly ICardFactory _cardFactory;
        private readonly SignalBus _signalBus;
        private readonly CanvasView _mainCanvasView;
        private readonly CardSettings _cardSettings;
        
        public CardInitializeSystem(
            ICardFactory cardFactory,
            SignalBus signalBus,
            CanvasView canvasView,
            CardSettings cardSettings
            )
        {
            _cardFactory = cardFactory;
            _signalBus = signalBus;
            _mainCanvasView = canvasView;
            _cardSettings = cardSettings;
        }

        private void InitializeCards(InitializeStartCardSignal initializeStartCardSignal)
        {
            foreach (var cardVo in _cardSettings.AllCards)
            {
                var card = _cardFactory.CreateCard(cardVo);
                card.transform.SetParent(_mainCanvasView.PlayerPanelView.transform, false);
            }
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<InitializeStartCardSignal>(InitializeCards);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<InitializeStartCardSignal>(InitializeCards);
        }
    }
}