using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pillz
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _loadGameButton;
        [SerializeField] private Button _settingsButton;

        private void Awake()
        {
            Debug.Assert(_newGameButton != null, nameof(_newGameButton) + " != null");
            Debug.Assert(_loadGameButton != null, nameof(_loadGameButton) + " != null");
            Debug.Assert(_settingsButton != null, nameof(_settingsButton) + " != null");
        }

        private void Start()
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            _loadGameButton.onClick.AddListener(OnLoadGameButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }

        private void OnDestroy()
        {
            _newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
            _loadGameButton.onClick.RemoveListener(OnLoadGameButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
        }

        private void OnNewGameButtonClicked()
        {
            SceneManager.LoadScene("Scenes/MainGame");
        }

        private void OnLoadGameButtonClicked() { }

        private void OnSettingsButtonClicked() { }
    }
}