using UnityEngine;

namespace Pillz
{
    public abstract class RoundSchedulerListener : MonoBehaviour
    {
        protected virtual void Start()
        {
            RegisterCallbacks();
        }

        private void OnDestroy()
        {
            UnregisterCallbacks();
        }

        private void RegisterCallbacks()
        {
            RoundScheduler.CountdownStart += SchedulerOnCountdownStart;
            RoundScheduler.CountdownTick += SchedulerOnCountdownTick;
            RoundScheduler.CountdownFinished += SchedulerOnCountdownFinished;
            RoundScheduler.RoundStart += SchedulerOnRoundStart;
            RoundScheduler.RoundTick += SchedulerOnRoundTick;
            RoundScheduler.RoundFinished += SchedulerOnRoundFinished;
        }

        private void UnregisterCallbacks()
        {
            RoundScheduler.CountdownStart -= SchedulerOnCountdownStart;
            RoundScheduler.CountdownTick -= SchedulerOnCountdownTick;
            RoundScheduler.CountdownFinished -= SchedulerOnCountdownFinished;
            RoundScheduler.RoundStart -= SchedulerOnRoundStart;
            RoundScheduler.RoundTick -= SchedulerOnRoundTick;
            RoundScheduler.RoundFinished -= SchedulerOnRoundFinished;
        }

        protected abstract void SchedulerOnCountdownStart(float startingSeconds);
        protected abstract void SchedulerOnCountdownTick(float remainingSeconds);
        protected abstract void SchedulerOnCountdownFinished();
        protected abstract void SchedulerOnRoundStart(float startingSeconds);
        protected abstract void SchedulerOnRoundTick(float remainingSeconds);
        protected abstract void SchedulerOnRoundFinished();
    }
}