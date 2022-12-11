using System.Collections.Generic;
using System.Linq;
using Db.Enums;
using PlayableItems;

namespace Game
{
    public class CardService
    {
        private readonly List<CardView> cards = new List<CardView>();

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
        }
    }
}