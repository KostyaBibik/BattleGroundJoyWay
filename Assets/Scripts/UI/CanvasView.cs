using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private PlayerCardHolder playerCardHolder;
        [SerializeField] private EnemyCardHolder enemyCardHolder;
        [SerializeField] private Button endTurn;
        [SerializeField] private Canvas canvas;
        
        [Inject] private SignalBus _signalBus;
        
        public PlayerCardHolder PlayerCardHolder => playerCardHolder;
        public EnemyCardHolder EnemyCardHolder => enemyCardHolder;
        public float PlaneDistance => canvas.planeDistance;

        private void Start()
        {
            InitializeBtn();
        }

        private void InitializeBtn()
        {
            endTurn.onClick.AddListener(EndMove);
        }

        private void EndMove()
        {
            _signalBus.Fire(new EndMotionSignal());
        }
    }
}
