using System.Collections.Generic;
using System.Linq;
using Db.Enums;
using PlayableItems;
using Signals;
using Zenject;

namespace Game
{
    public class CardService
    {
        private readonly List<CardView> cards = new List<CardView>();
        private readonly SignalBus _signalBus;

        public CardService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void AddCard(CardView cardView)
        {
            cards.Add(cardView);
            cardView.HealthComponent.onDie += RemoveCard;
        }

        public List<CardView> GetCardsByTeam(ETeam team)
        {
            return cards.Where(card => card.GetTeam() == team).ToList();
        }

        private void RemoveCard(CardView cardView)
        {
            cards.Remove(cardView);

            CheckForRemainingCardsByTeam(cardView.GetTeam());
        }

        private void CheckForRemainingCardsByTeam(ETeam team)
        {
            if (GetCardsByTeam(team).Count <= 0)
            {
                switch (team)
                {
                    case ETeam.Player:
                        _signalBus.Fire(new LoseSignal());
                        break;
                    case ETeam.Enemy:
                        _signalBus.Fire(new WinSignal());
                        break;
                }
            }
        }
    }
}