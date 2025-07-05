using System;

namespace OfferSystem
{
    public class DateTrigger : IOfferTrigger
    {
        public DateTime StartDate { get; }
        public TimeSpan Duration { get; }
        public bool HasStarted { get; private set; }
        public bool HasEnded { get; private set; }

        public DateTrigger(DateTime startDate, TimeSpan duration)
        {
            StartDate = startDate;
            Duration = duration;
        }

        public void Trigger()
        {
            HasStarted = true;
        }

        public void Stop()
        {
            HasEnded = true;
        }

        public bool IsActive()
        {
            return HasStarted && !HasEnded;
        }
    }
}