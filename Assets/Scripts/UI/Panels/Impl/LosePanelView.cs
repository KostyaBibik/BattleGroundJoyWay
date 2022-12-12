using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Panels.Impl
{
    public class LosePanelView : UiPanel
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button exitBtn;
        
        private void Start()
        {
            exitBtn.onClick.AddListener(delegate
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            });
            restartBtn.onClick.AddListener(delegate
            {
                SceneManager.LoadScene(0);
            });
        }
    }
}