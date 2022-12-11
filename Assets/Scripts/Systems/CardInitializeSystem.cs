using System;
using System.Collections.Generic;
using Db;
using Db.Enums;
using Db.Impl;
using Game;
using Game.Impl;
using PlayableItems.Logic;
using Signals;
using UI;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Systems
{
    public class CardInitializeSystem : IInitializable, IDisposable
    {
        private readonly ICardFactory _cardFactory;
        private readonly SignalBus _signalBus;
        private readonly CanvasView _mainCanvasView;
        private readonly CardSettings _cardSettings;
        private readonly CardService _cardService;
        
        public CardInitializeSystem(
            ICardFactory cardFactory,
            SignalBus signalBus,
            CanvasView canvasView,
            CardSettings cardSettings,
            CardService cardService
            )
        {
            _cardFactory = cardFactory;
            _signalBus = signalBus;
            _mainCanvasView = canvasView;
            _cardSettings = cardSettings;
            _cardService = cardService;
        }

        private void InitializeCards(InitializeStartCardSignal initializeStartCardSignal)
        {
            foreach (var cardVo in _cardSettings.AllCards)
            {
                var card = _cardFactory.CreateCard(cardVo);
                
                var parentTransform = default(Transform);
                switch (cardVo.Team)
                {
                    case ETeam.Enemy:
                        parentTransform = _mainCanvasView.EnemyCardHolder.transform;
                        break;
                    case ETeam.Player:
                        parentTransform = _mainCanvasView.PlayerCardHolder.transform;
                        break;
                }
                
                card.transform.SetParent(parentTransform, false);
                _cardService.AddCard(card);
            }

            var listActions = new List<Tuple<EActionType, IActionState>>
            {
                new (EActionType.Attack, new AttackActionState()),
                new (EActionType.Defend, new DefenceActionState()),
                new (EActionType.Heal, new HealActionState()),
                new (EActionType.Poison, new PoisonActionState())
            };

            foreach (var playerCard in _cardService.GetCardsByTeam(ETeam.Player))
            {
                var action = listActions[Random.Range(0, listActions.Count)];
                
                playerCard.SetAction(action.Item1, action.Item2);
                listActions.Remove(action);
            }
            
            foreach (var aiCard in _cardService.GetCardsByTeam(ETeam.Enemy))
            {
                aiCard.SetAction(EActionType.Attack, new AttackActionState());
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