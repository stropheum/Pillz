using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Pillz.UI
{
    public class CountdownTimer : RoundSchedulerListener
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private int _lastSecondValue;

        private void Awake()
        {
            Debug.Assert(_timerText != null, nameof(_timerText) + " != null");
        }

        protected override void SchedulerOnCountdownStart(float remainingSeconds)
        {
            _timerText.text = Mathf.CeilToInt(remainingSeconds).ToString();
            _timerText.rectTransform.DOPunchScale(Vector3.one * 1.25f, 0.25f);
            _lastSecondValue = (int)remainingSeconds;
        }

        protected override void SchedulerOnCountdownTick(float remainingSeconds)
        {
            int oldSecondValue = _lastSecondValue;
            _lastSecondValue = Mathf.CeilToInt(remainingSeconds);

            if (oldSecondValue == _lastSecondValue)
            {
                return;
            }

            _timerText.text = _lastSecondValue.ToString();
            _timerText.rectTransform.DOPunchScale(Vector3.one * 1.25f, 0.25f);
        }

        protected override void SchedulerOnCountdownFinished()
        {
            StartCoroutine(FinishCountdown());
        }

        protected override void SchedulerOnRoundStart(float remainingSeconds)
        {
        }

        protected override void SchedulerOnRoundTick(float remainingSeconds)
        {
        }

        protected override void SchedulerOnRoundFinished()
        {
        }

        private IEnumerator FinishCountdown()
        {
            _timerText.text = "SURVIVE! D:<";
            yield return new WaitForSeconds(1f);
            yield return _timerText.rectTransform.DOScale(0f, 0.25f).SetEase(Ease.OutSine).WaitForCompletion();
            Destroy(gameObject);
        }
    }
}