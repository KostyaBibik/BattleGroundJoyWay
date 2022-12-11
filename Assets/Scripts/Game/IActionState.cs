using Db.Actions;
using Db.Enums;
using PlayableItems;

namespace Game
{
    public interface IActionState
    {
        IActionState Initialize(CardView cardView, IActionBase actionBase);
        
        public void OnEndDrag(CardView targetCard);
        ETargetAction TargetAction { get; }
    }
}