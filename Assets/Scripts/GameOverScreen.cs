using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pillz
{
    public class GameOverScreen : MonoBehaviour
    {
        [SerializeField] private Button _quitButton;

        private void Awake()
        {
            Debug.Assert(_quitButton != null, nameof(_quitButton) + " != null");
        }

        private void Start()
        {
            _quitButton.onClick.AddListener(OnQuitButtonClick);
        }

        private void OnDestroy()
        {
            _quitButton.onClick.RemoveListener(OnQuitButtonClick);
        }

        private void OnQuitButtonClick()
        {
            GameplayUIController.Instance.HideAllScreens();
            SceneManager.LoadScene("MainMenu");
        }
    }
}