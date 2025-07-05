using System;
using System.Collections.Generic;

namespace OfferSystem
{
    public class TriggerManager
    {
        public event Action<IOfferTrigger> OnTriggerFired;
        public event Action<IOfferTrigger> OnTriggerEnded;

        private readonly List<DateTrigger> dateTriggers = new List<DateTrigger>();
        private readonly List<EventTrigger> eventTriggers = new List<EventTrigger>();

        public void FireTrigger(string eventName)
        {
            IOfferTrigger trigger = GetEventTrigger(eventName);
            trigger.Trigger();
            OnTriggerFired?.Invoke(trigger);
        }

        private EventTrigger GetEventTrigger(string eventName)
        {
            foreach (EventTrigger eventTrigger in eventTriggers)
            {
                if (eventTrigger.EventName == eventName)
                {
                    return eventTrigger;
                }
            }

            return null;
        }

        public void RegisterTrigger(IOfferTrigger trigger)
        {
            if (trigger is DateTrigger dateTrigger)
            {
                RegisterDateTrigger(dateTrigger);
            }

            if (trigger is EventTrigger eventTrigger)
            {
                RegisterEventTrigger(eventTrigger);
            }
        }

        private void RegisterDateTrigger(DateTrigger trigger)
        {
            if (dateTriggers.Contains(trigger))
            {
                return;
            }

            dateTriggers.Add(trigger);
        }

        private void RegisterEventTrigger(EventTrigger trigger)
        {
            if (eventTriggers.Contains(trigger))
            {
                return;
            }

            eventTriggers.Add(trigger);
        }

        public void Update()
        {
            DateTime now = DateTime.UtcNow;

            foreach (DateTrigger trigger in dateTriggers)
            {
                if (!trigger.HasStarted && now >= trigger.StartDate)
                {
                    trigger.Trigger();
                    OnTriggerFired?.Invoke(trigger);
                }

                DateTime endDate = trigger.StartDate + trigger.Duration;
                if (trigger.HasStarted && !trigger.HasEnded && now >= endDate)
                {
                    trigger.Stop();
                    OnTriggerEnded?.Invoke(trigger);
                    dateTriggers.Remove(trigger);
                }
            }
        }
    }
}