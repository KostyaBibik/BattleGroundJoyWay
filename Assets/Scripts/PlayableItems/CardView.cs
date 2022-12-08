using Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace PlayableItems
{
    public class CardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public CanvasGroup canvasGroup;
        private CardMovingSystem _movingSystem;
        
        public CanvasGroup CanvasGroup => canvasGroup;

        [Inject]
        public void Construct(CardMovingSystem movingSystem)
        {
            _movingSystem = movingSystem;
        }

        public void SetHealth(int value)
        {
            
        }
        
        public void SetName(string value)
        {
            
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