using UnityEngine;

namespace UI
{
    public class CanvasView : MonoBehaviour
    {
        [SerializeField] private PlayerPanelView playerPanelView;
        [SerializeField] private EnemyPanelView enemyPanelView;

        public PlayerPanelView PlayerPanelView => playerPanelView;
        public EnemyPanelView EnemyPanelView => enemyPanelView;
    }
}
