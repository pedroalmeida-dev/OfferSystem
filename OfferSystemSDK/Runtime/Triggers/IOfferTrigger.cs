namespace OfferSystem
{
    public interface IOfferTrigger
    {
        void Trigger();
        bool IsActive();
    }
}