using UnityEngine;

namespace Pillz
{
    public class PillDispenser : RoundSchedulerListener
    {
        [SerializeField] private GameObject _pillPrefab;

        private void Awake()
        {
            Debug.Assert(_pillPrefab != null, nameof(_pillPrefab) + " != null");
        }

        public void RequestPill()
        {
            
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
        }

        protected override void SchedulerOnRoundFinished()
        {
        }
    }
}