namespace OfferSystem
{    
    public class EventTrigger : IOfferTrigger
    {
        public string EventName { get; }

        public bool HasTriggered;

        public EventTrigger(string eventName)
        {
            EventName = eventName;
        }

        public void Trigger()
        {
            HasTriggered = true;
        }

        public bool IsActive()
        {
            return HasTriggered;
        }
    }
}