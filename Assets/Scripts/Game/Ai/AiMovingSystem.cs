using System;
using System.Collections;
using System.Collections.Generic;
using Db.Enums;
using PlayableItems;
using Signals;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Ai
{
    public class AiMovingSystem : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly CardService _cardService;
        private readonly Camera _camera;

        private List<CardView> _aiCards = new List<CardView>();
        private EventSystem _eventSystem;
        private bool _aiTurnMove;
        
        public AiMovingSystem(
            CardService cardService,
            SignalBus signalBus,
            Camera camera
            )
        {
            _cardService = cardService;
            _signalBus = signalBus;
            _camera = camera;
        }

        public void Initialize()
        {
            _eventSystem = EventSystem.current;
            _signalBus.Subscribe<EndMotionSignal>(CheckForMoving);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EndMotionSignal>(CheckForMoving);
        }

        private void CheckForMoving(EndMotionSignal motionSignal)
        {
            _aiTurnMove = !_aiTurnMove;
            if (!_aiTurnMove)
            {
                return;
            }

            Observable.FromCoroutine(StartMove)
                .Subscribe();
        }

        private IEnumerator StartMove()
        {
            yield return new WaitForSeconds(2f);

            _aiCards = _cardService.GetCardsByTeam(ETeam.Enemy);
            var playerCards = _cardService.GetCardsByTeam(ETeam.Player);

            foreach (var aiCard in _aiCards)
            {
                var randomPlayerCard = playerCards[Random.Range(0, playerCards.Count)];
                var pointer = new PointerEventData(_eventSystem);
                var startPos = _camera.WorldToScreenPoint(aiCard.transform.position);
                pointer.position = startPos;
                
                aiCard.OnBeginDrag(pointer);
                
                var timeTarget = 1f;
                var timeDragging = 0f;
                var progress = 0f;
                var targetPos = _camera.WorldToScreenPoint(randomPlayerCard.transform.position);
                
                do
                {
                    timeDragging += Time.deltaTime;
                    progress = timeDragging / timeTarget;
                    pointer.position = Vector2.Lerp(startPos, targetPos, progress);
                    aiCard.OnDrag(pointer);
                    yield return null;

                } while (progress <= timeTarget);
                
                yield return new WaitForSeconds(.5f);
                
                randomPlayerCard.OnDrop(pointer);
                aiCard.OnEndDrag(pointer);
            }
            
            yield return new WaitForSeconds(1f);
            
            _signalBus.Fire<EndMotionSignal>();
        }
    }
}