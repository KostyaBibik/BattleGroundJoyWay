using System;
using Db.Enums;
using Game;
using Signals;
using Zenject;

namespace Systems
{
    public class IdentifyMotionSystem : IInitializable, IDisposable
    {
        private ETeam _currentMotionTeam;
        
        private readonly SignalBus _signalBus;
        private readonly CardService _cardService;
        
        public IdentifyMotionSystem(
            SignalBus signalBus,
            CardService cardService
            )
        {
            _signalBus = signalBus;
            _cardService = cardService;
        }
        
        public void Initialize()
        {
            _signalBus.Subscribe<EndMotionSignal>(OnCatchEndMotionSignal);
            _signalBus.Subscribe<StartGameSignal>(OnStartGame);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EndMotionSignal>(OnCatchEndMotionSignal);
            _signalBus.Unsubscribe<StartGameSignal>(OnStartGame);
        }

        private void OnCatchEndMotionSignal(EndMotionSignal signal)
        {
            _currentMotionTeam = _currentMotionTeam == ETeam.Enemy ? ETeam.Player : ETeam.Enemy;
            SetTeamMoving(_currentMotionTeam);
        }

        private void OnStartGame(StartGameSignal startGameSignal)
        {
            SetTeamMoving(ETeam.Player);
        }

        private void SetTeamMoving(ETeam team)
        {
            var deactivateTeam = team == ETeam.Enemy ? ETeam.Player : ETeam.Enemy;
            var deactivatedCards = _cardService.GetCardsByTeam(deactivateTeam);
            foreach (var deactivatedCard in deactivatedCards)
            {
                deactivatedCard.SwitchActiveState(false);
            }
            
            var movableCards = _cardService.GetCardsByTeam(team);
            foreach (var movableCard in movableCards)
            {
                movableCard.SwitchActiveState(true);
            }
        }
    }
}