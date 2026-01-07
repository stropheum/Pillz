using TMPro;
using UnityEngine;

namespace Pillz.UI
{
    public class RoundTimer : RoundSchedulerListener
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] [Range(0f, 100f)] private float _fractionalTextSizePercent = 50f;

        private void Awake()
        {
            Debug.Assert(_timerText != null, nameof(_timerText) + " != null");
        }

        protected override void SchedulerOnCountdownStart(float remainingSeconds)
        {
        }

        protected override void SchedulerOnCountdownTick(float remainingSeconds)
        {
        }

        protected override void SchedulerOnCountdownFinished()
        {
        }

        protected override void SchedulerOnRoundStart(float remainingSeconds)
        {
            SetTimerText(remainingSeconds);
        }

        protected override void SchedulerOnRoundTick(float remainingSeconds)
        {
            SetTimerText(remainingSeconds);
        }

        protected override void SchedulerOnRoundFinished()
        {
            Destroy(gameObject);
        }

        private void SetTimerText(float remainingSeconds)
        {
            int minutes = (int)remainingSeconds / 60;
            int seconds = (int)remainingSeconds % 60;
            int fractionalSeconds = (int)(remainingSeconds * 100) % 100;

            _timerText.text =
                $"{minutes}:{seconds:00}.<size={_fractionalTextSizePercent}%>{fractionalSeconds:00}</size>";
        }
    }
}