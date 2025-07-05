using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace OfferSystem
{
    public class OfferManager : MonoBehaviour
    {
        public static OfferManager Instance { get; private set; }

        public event Action<Offer> OnOfferActivated;
        public event Action<Offer> OnOfferDeactivated;

        private Dictionary<string, Offer> offers = new Dictionary<string, Offer>();
        private List<Offer> activeOffers = new List<Offer>();

        private ValidationManager validationManager;
        private TriggerManager triggerManager;
        public TriggerManager TriggerManager => triggerManager;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            triggerManager = new TriggerManager();

            triggerManager.OnTriggerFired += HandleTriggerFired;
            triggerManager.OnTriggerEnded += HandleTriggerEnded;
        }

        private void OnDestroy()
        {
            if (triggerManager != null)
            {
                triggerManager.OnTriggerFired -= HandleTriggerFired;
                triggerManager.OnTriggerEnded -= HandleTriggerEnded;
            }
        }

        public void Initialize(List<Offer> offers, ValidationManager validationManager)
        {
            this.offers = offers.ToDictionary(offer => offer.Id, offer => offer);
            this.validationManager = validationManager;

            foreach (Offer offer in offers)
            {
                if (offer.Triggers.Count == 0)
                {
                    ActivateValidOffer(offer);
                }

                foreach (IOfferTrigger trigger in offer.Triggers)
                {
                    triggerManager.RegisterTrigger(trigger);
                }

                offer.PrechainedOffer = PrechainedOffer(offer.Id);
            }
        }

        private Offer PrechainedOffer(string chainedOfferId)
        {
            foreach(Offer offer in offers.Values)
            {
                if (offer.NextOfferId == chainedOfferId)
                {
                    return offer;
                }
            }

            return null;
        }

        private void Update()
        {
            triggerManager.Update();
        }

        private void HandleTriggerFired(IOfferTrigger trigger)
        {
            foreach (Offer offer in offers.Values)
            {
                if (!offer.Triggers.Contains(trigger))
                {
                    continue;
                }

                Debug.Log($"[OfferManager] Trigger received for Offer: {offer.Id}");

                bool allTriggersActive = false;
                foreach (IOfferTrigger offerTrigger in offer.Triggers)
                {
                    allTriggersActive |= offerTrigger.IsActive();
                }

                if (!allTriggersActive)
                {
                    continue;
                }

                if (IsAnyOfferOnChainActive(offer))
                {
                    continue;
                }

                ActivateValidOffer(offer);
            }
        }

        private bool IsAnyOfferOnChainActive(Offer offer)
        {
            if (offer.PrechainedOffer == null || offer.PrechainedOffer == offer)
            {
                return false;
            }

            Offer preChainedOffer = offer.PrechainedOffer;

            while (preChainedOffer != offer && preChainedOffer != null)
            {
                if (preChainedOffer.IsOfferActive)
                {
                    return true;
                }

                preChainedOffer = preChainedOffer.PrechainedOffer;
            }

            return false;
        }

        private void ActivateValidOffer(Offer offer)
        {
            if (!IsOfferValid(offer))
            {
                return;
            }

            ActivateOffer(offer);
        }

        private bool IsOfferValid(Offer offer)
        {
            return validationManager.ValidateOffer(offer);
        }

        private void ActivateOffer(Offer offer)
        {
            if (activeOffers.Contains(offer))
            {
                return;
            }

            Debug.Log($"[OfferManager] Activating offer: {offer.Id}");
            offer.IsOfferActive = true;
            activeOffers.Add(offer);

            OnOfferActivated?.Invoke(offer);
        }

        private void HandleTriggerEnded(IOfferTrigger trigger)
        {
            foreach (Offer offer in offers.Values)
            {
                if (!offer.Triggers.Contains(trigger))
                {
                    continue;
                }

                Debug.Log($"[OfferManager] Trigger ended for Offer: {offer.Id}");

                DeactivateOffer(offer);
            }
        }

        private void DeactivateOffer(Offer offer)
        {
            if (!activeOffers.Contains(offer))
            {
                return;
            }

            Debug.Log($"[OfferManager] Deactivating offer: {offer.Id}");
            offer.IsOfferActive = false;
            activeOffers.Remove(offer);

            OnOfferDeactivated?.Invoke(offer);
        }

        public void OnOfferPurchased(string offerId)
        {
            if (!offers.TryGetValue(offerId, out Offer offer))
            {
                return;
            }

            DeactivateOffer(offer);

            if (string.IsNullOrEmpty(offer.NextOfferId))
            {
                return;
            }

            if (offers.TryGetValue(offer.NextOfferId, out var nextOffer))
            {
                Debug.Log($"[OfferManager] Unlocking next offer: {nextOffer.Id}");
                ActivateOffer(nextOffer);
            }
        }

        public List<Offer> GetActiveOffers()
        {
            return new List<Offer>(activeOffers);
        }

        public List<Offer> GetAllOffers()
        {
            return new List<Offer>(offers.Values);
        }

        public Offer GetOffer(string offerId)
        {
            Offer offer = null;
            offers.TryGetValue(offerId, out offer);
            return offer;
        }
    }

}