using OfferSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectCode
{
    public class OfferView : MonoBehaviour
    {
        [SerializeField]
        private GameObject offerObject;
        [SerializeField]
        private TMP_Text offerTitle;
        [SerializeField]
        private Button button;

        private Offer offer;
        
        private void Start()
        {
            OfferManager.Instance.OnOfferActivated += OnOfferActivated;
            OfferManager.Instance.OnOfferDeactivated += OnOfferDeactivated;

            button.onClick.AddListener(PurchaseOffer);
        }

        private void OnOfferActivated(Offer offer)
        {
            this.offer = offer; 
            SetupOffer(offer);
        }

        private void OnOfferDeactivated(Offer offer)
        {
            offerObject.SetActive(false);
        }

        private void SetupOffer(Offer offer)
        {
            offerObject.SetActive(true);
            offerTitle.SetText(offer.Id);
        }

        private void PurchaseOffer()
        {
            OfferManager.Instance.OnOfferPurchased(offer.Id);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(PurchaseOffer);

            OfferManager.Instance.OnOfferActivated -= OnOfferActivated;
            OfferManager.Instance.OnOfferDeactivated -= OnOfferDeactivated;
        }
    }
}