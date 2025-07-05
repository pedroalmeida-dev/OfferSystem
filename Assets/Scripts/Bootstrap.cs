using OfferSystem;
using ProjectCode.OfferConditions;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectCode
{
    public class Bootstrap : MonoBehaviour
    {
        private void Start()
        {
            List<Offer> offers = OfferNetworkManager.FetchAvailableOffers();
            ValidationManager validationManager = CreateValidationManager();
            OfferManager.Instance.Initialize(offers, validationManager);

            Invoke("EnterMainMenu", 1);
        }

        private ValidationManager CreateValidationManager()
        {
            IValidationCondition condition = new MinLevelCondition();

            return new ValidationManager(condition);
        }

        private void EnterMainMenu()
        {
            OfferManager.Instance.TriggerManager.FireTrigger("main_menu_enter");
        }
    }
}