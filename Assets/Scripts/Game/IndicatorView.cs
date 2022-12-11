using UI;
using UnityEngine;
using Zenject;

namespace Game
{
    public class IndicatorView : MonoBehaviour, IInitializable
    {
        [SerializeField] private LineRenderer lineRenderer;

        [Inject] private CanvasView _canvas;

        private float _posZPoints;

        public void SetPoint(int index, Vector3 point)
        {
            point.z = _posZPoints;
            lineRenderer.SetPosition(index, point);
        }

        public void RefreshIndicator()
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }

        public void Initialize()
        {
            _posZPoints = _canvas.PlaneDistance;
        }
    }
}