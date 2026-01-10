using System;
using UnityEngine;

namespace Pillz
{
    public class Pill : RoundSchedulerListener
    {
        public enum PillColor { Red, Blue, Yellow }

        [SerializeField] private PillHalf _firstHalf;
        [SerializeField] private PillHalf _secondHalf;
        
        private bool _isControllable;

        private void OnEnable()
        {
            Debug.Assert(_firstHalf != null, "First pill half must be assigned in the inspector.");
            Debug.Assert(_secondHalf != null, "Second pill half must be assigned in the inspector.");
        }

        public void SetControllable(bool isControllable)
        {
            _isControllable = isControllable;
        }

        public void SetColors(Tuple<Color, Color> pillColor)
        {
            _firstHalf.SetColor(pillColor.Item1);
            _secondHalf.SetColor(pillColor.Item2);
        }
        
        protected override void SchedulerOnCountdownStart(float startingSeconds)
        {
        }

        protected override void SchedulerOnCountdownTick(float remainingSeconds)
        {
        }

        protected override void SchedulerOnCountdownFinished()
        {
        }

        protected override void SchedulerOnRoundStart(float startingSeconds)
        {
        }

        protected override void SchedulerOnRoundTick(float remainingSeconds)
        {
            transform.position += Vector3.down;
        }

        protected override void SchedulerOnRoundFinished()
        {
        }
    }
}