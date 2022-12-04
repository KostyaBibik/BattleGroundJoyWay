using System;
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
        
        public CardInitializeSystem(
            ICardFactory cardFactory,
            SignalBus signalBus,
            CanvasView canvasView
            )
        {
            _cardFactory = cardFactory;
            _signalBus = signalBus;
            _mainCanvasView = canvasView;
        }

        private void InitializeCards(InitializeStartCardSignal initializeStartCardSignal)
        {
            for (var i = 0; i < 5; i++)
            {
                var card = _cardFactory.CreateCard();
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