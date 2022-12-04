using Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PlayableItems
{
    public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CardMovingSystem _movingSystem;

        [Inject]
        public void Construct(CardMovingSystem movingSystem)
        {
            _movingSystem = movingSystem;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _movingSystem.OnBeginDrag(eventData, this);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            _movingSystem.OnDrag(eventData, this);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            _movingSystem.OnEndDrag(eventData, this);
        }
    }
}