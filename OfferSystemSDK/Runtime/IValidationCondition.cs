namespace OfferSystem
{
    public interface IValidationCondition
    {
        string Id { get; }
        bool IsValid(string key, float value);
    }
}