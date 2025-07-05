using System.Collections.Generic;
using WalletTransation;

namespace OfferSystem
{ 
    public class Offer
    {
        public readonly string Id;
        public readonly string TransactionId;
        public readonly List<IOfferTrigger> Triggers;
        public readonly List<OfferValidationData> Validations;
        public readonly string NextOfferId;
        
        internal Offer PrechainedOffer;
        internal bool IsOfferActive;

        public bool IsActive => IsOfferActive;

        public Offer(string id, string transactionId, string nextOfferId = null)
        {
            Id = id;
            TransactionId = transactionId;
            NextOfferId = nextOfferId;
        }

        public Offer(string id, string transactionId, List<IOfferTrigger> triggers, List<OfferValidationData> validations, string nextOfferId = null)
        {
            Id = id;
            TransactionId = transactionId;
            Triggers = triggers;
            Validations = validations;
            NextOfferId = nextOfferId;
        }

        public Offer(string id, string transactionId, IOfferTrigger trigger, OfferValidationData validation, string nextOfferId = null)
        {
            Id = id;
            TransactionId = transactionId;
            Triggers = new List<IOfferTrigger>() { trigger };
            Validations = new List<OfferValidationData>() { validation };
            NextOfferId = nextOfferId;
        }
    }
}