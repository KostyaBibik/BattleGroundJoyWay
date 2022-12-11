using Systems;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace UI
{
    public class PlayerPanelView : MonoBehaviour, IDropHandler, IDropPanel
    {
        [Inject] private CardMovingSystem _movingSystem;
        public Transform parentForDrops => transform;

        public void OnDrop(PointerEventData eventData)
        {
           // _movingSystem.OnDrop(eventData, this);
        }
    }
}
