using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Pillz
{
    public class VictoryScreen : MonoBehaviour
    {
        [SerializeField] private Button _continueButton;

        private void Awake()
        {
            Debug.Assert(_continueButton != null, nameof(_continueButton) + " != null");
        }

        private void Start()
        {
            _continueButton.onClick.AddListener(OnReturnButtonClick);
        }

        private void OnDestroy()
        {
            _continueButton.onClick.RemoveListener(OnReturnButtonClick);
        }

        private void OnReturnButtonClick()
        {
            GameplayUIController.Instance.HideAllScreens();
            SceneManager.LoadScene("MainMenu");
        }
    }
}