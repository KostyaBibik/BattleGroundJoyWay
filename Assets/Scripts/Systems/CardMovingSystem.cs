using PlayableItems;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Systems
{
    public class CardMovingSystem
    {
        private readonly Camera _camera;
        private Vector3 _offsetTaking;
        private CardView _selectedCard;
        
        public CardMovingSystem(Camera camera)
        {
            _camera = camera;
        }
        
        public void OnBeginDrag(PointerEventData eventData, CardView cardView)
        {
            _selectedCard = cardView;
            _offsetTaking = cardView.transform.position - _camera.ScreenToWorldPoint(eventData.position);
        }
        
        public void OnDrag(PointerEventData eventData, CardView cardView)
        {
            var newPos = _camera.ScreenToWorldPoint(eventData.position);
            cardView.transform.position = (Vector2)(newPos + _offsetTaking);
        }
        
        public void OnEndDrag(PointerEventData eventData, CardView cardView)
        {
            
        }
        
        public void OnDrop(PointerEventData eventData, IDropPanel dropPanel)
        {
            Debug.Log(_selectedCard.name + " : " + dropPanel.parentForDrops.name);
            _selectedCard.transform.SetParent(dropPanel.parentForDrops);
            
            _selectedCard = null;
            Debug.Log("OnDrop");
        }
    }
}