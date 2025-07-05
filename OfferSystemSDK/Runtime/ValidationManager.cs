using System.Collections.Generic;
using System.Linq;

namespace OfferSystem
{
    public class ValidationManager
    {
        private Dictionary<string, IValidationCondition> conditions;

        public ValidationManager(params IValidationCondition[] conditions)
        {
            this.conditions = conditions.ToDictionary(condition => condition.Id, condition => condition);
        }

        public bool ValidateOffer(Offer offer)
        {
            foreach (OfferValidationData validation in offer.Validations)
            {
                if (!conditions.TryGetValue(validation.Id, out IValidationCondition condition))
                {
                    UnityEngine.Debug.LogError($"[ValidationManager] Validation with id: {validation.Id} not found");
                    return false;
                }

                if (!condition.IsValid(validation.Key, validation.Value))
                {
                    return false;
                }
            }

            return true;
        }
    }
}