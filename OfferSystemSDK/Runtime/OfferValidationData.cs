namespace OfferSystem
{
    public class OfferValidationData
    {
        public readonly string Id;
        public readonly string Key;
        public readonly float Value;

        public OfferValidationData(string id, string key, float value)
        {
            Id = id;
            Key = key;
            Value = value;
        }
    }
}