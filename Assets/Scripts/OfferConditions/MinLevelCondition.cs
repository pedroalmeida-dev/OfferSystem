using OfferSystem;

namespace ProjectCode.OfferConditions
{
    public class MinLevelCondition : IValidationCondition
    {
        public string Id
        {
            get => "min_user_level";
        }

        public bool IsValid(string key, float value)
        {
            return value < UserData.Level;
        }
    }
}