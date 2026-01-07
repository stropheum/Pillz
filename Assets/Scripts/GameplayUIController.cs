using UnityEngine;

namespace Pillz
{
    public class GameplayUIController : Singleton<GameplayUIController>
    {
        [SerializeField] private Canvas _gameOverScreen;
        [SerializeField] private Canvas _victoryScreen;

        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(_gameOverScreen != null, nameof(_gameOverScreen) + " != null");
            Debug.Assert(_victoryScreen != null, nameof(_victoryScreen) + " != null");
        }

        public void ShowGameOverScreen()
        {
            ShowScreenExclusive(_gameOverScreen);
        }

        public void HideGameOverScreen()
        {
            _gameOverScreen.enabled = false;
        }

        public void ShowVictoryScreen()
        {
            ShowScreenExclusive(_victoryScreen);
        }

        public void HideVictoryScreen()
        {
            _victoryScreen.enabled = false;
        }

        public void HideAllScreens()
        {
            ShowScreenExclusive(null);
        }

        private void ShowScreenExclusive(Canvas canvas)
        {
            _gameOverScreen.enabled = _gameOverScreen == canvas;
            _victoryScreen.enabled = _victoryScreen == canvas;
        }
    }
}