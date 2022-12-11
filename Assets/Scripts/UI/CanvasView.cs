using System;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private PlayerPanelView playerPanelView;
        [SerializeField] private EnemyPanelView enemyPanelView;
        [SerializeField] private Button endTurn;
        [SerializeField] private Canvas canvas;
        
        [Inject] private SignalBus _signalBus;
        
        public PlayerPanelView PlayerPanelView => playerPanelView;
        public EnemyPanelView EnemyPanelView => enemyPanelView;
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
