using System;
using System.Collections;
using Pillz.UI;
using UnityEngine;

namespace Pillz
{
    public class RoundScheduler : MonoBehaviour
    {
        [SerializeField] private CountdownTimer _countdownTimerPrefab;
        [SerializeField] private RoundTimer _roundTimerPrefab;
        [SerializeField] private int _countdownTimeInSeconds = 3;
        [SerializeField] private int _roundTimeInSeconds = 600;
        [SerializeField] private float _introWaitTimeInSeconds = 2f;
        [SerializeField] private float _tickInterval = 0.25f;

        public static event Action<float> CountdownStart;
        public static event Action<float> RoundStart;
        public static event Action<float> CountdownTick;
        public static event Action<float> RoundTick;
        public static event Action CountdownFinished;
        public static event Action RoundFinished;

        private RoundPhase _currentPhase;

        private void Awake()
        {
            Debug.Assert(_countdownTimerPrefab != null, nameof(_countdownTimerPrefab) + " != null");
            Debug.Assert(_roundTimerPrefab != null, nameof(_roundTimerPrefab) + " != null");

            Instantiate(_countdownTimerPrefab);
            Instantiate(_roundTimerPrefab);
        }

        public void StartRound()
        {
            Debug.Assert(_currentPhase is RoundPhase.None or RoundPhase.Completed, "Cannot start a round when one is already active");
            StartCoroutine(StartRoundRoutine());
        }

        private IEnumerator StartRoundRoutine()
        {
            yield return new WaitForSeconds(_introWaitTimeInSeconds);
            yield return PerformCountdown();
            yield return PerformRound();
            yield return PerformRoundCompletion();
        }

        private IEnumerator PerformCountdown()
        {
            _currentPhase = RoundPhase.Countdown;
            float remainingTime = _tickInterval;
            CountdownStart?.Invoke(remainingTime);

            while (remainingTime > 0f)
            {
                remainingTime -= Time.deltaTime;
                CountdownTick?.Invoke(remainingTime);
                yield return null;
            }

            remainingTime = 0f;
            CountdownTick?.Invoke(remainingTime);
            CountdownFinished?.Invoke();

            yield return null;
        }

        private IEnumerator PerformRound()
        {
            _currentPhase = RoundPhase.Active;
            float remainingTime = _roundTimeInSeconds;
            RoundStart?.Invoke(remainingTime);

            while (remainingTime > 0f)
            {
                remainingTime -= Time.deltaTime;
                RoundTick?.Invoke(remainingTime);
                yield return null;
            }

            remainingTime = 0f;
            RoundTick?.Invoke(remainingTime);
            RoundFinished?.Invoke();

            yield return null;
        }

        private IEnumerator PerformRoundCompletion()
        {
            _currentPhase = RoundPhase.Completed;
            yield return null;
        }
    }

    public enum RoundPhase
    {
        None,
        Countdown,
        Active,
        Completed
    }
}