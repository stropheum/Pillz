using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pillz.UI
{
    public class MainMenuRootPanel : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _unlocksButton;
        [SerializeField] private Button _settingsButton;

        private void Awake()
        {
            Debug.Assert(_startButton != null, nameof(_startButton) + " != null");
            Debug.Assert(_unlocksButton != null, nameof(_unlocksButton) + " != null");
            Debug.Assert(_settingsButton != null, nameof(_settingsButton) + " != null");
            
            
            _startButton.onClick.AddListener(StartButtonOnClick);
        }

        private void StartButtonOnClick()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
