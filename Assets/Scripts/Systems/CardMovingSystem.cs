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
        private Transform _savedDropPanel;
        
        public CardMovingSystem(Camera camera)
        {
            _camera = camera;
        }
        
        public void OnBeginDrag(PointerEventData eventData, CardView cardView)
        {
            _selectedCard = cardView;
            _savedDropPanel = _selectedCard.transform.parent;
            var inputPoint = new Vector3(eventData.position.x, eventData.position.y, 0f);
            _offsetTaking = cardView.transform.position - _camera.ScreenToWorldPoint(inputPoint);
            _selectedCard.canvasGroup.blocksRaycasts = false;
        }
        
        public void OnDrag(PointerEventData eventData, CardView cardView)
        {
            var inputPoint = new Vector3(eventData.position.x, eventData.position.y, 0f);
            var newPos = _camera.ScreenToWorldPoint(inputPoint);
            cardView.transform.position = (newPos + _offsetTaking);
            
        }
        
        public void OnEndDrag(PointerEventData eventData, CardView cardView)
        {
            cardView.transform.SetParent(_savedDropPanel);
            _selectedCard.canvasGroup.blocksRaycasts = true;
            Debug.Log("OnEndDrag");
        }
        
        public void OnDrop(PointerEventData eventData, IDropPanel dropPanel)
        {
            Debug.Log(_selectedCard.name + " : " + dropPanel.parentForDrops.name);
            _savedDropPanel = dropPanel.parentForDrops;
            Debug.Log("OnDrop");
        }
    }
}