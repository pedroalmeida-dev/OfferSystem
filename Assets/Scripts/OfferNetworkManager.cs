using OfferSystem;
using System;
using System.Collections.Generic;
using WalletTransation;

namespace ProjectCode
{
    public static class OfferNetworkManager
    {
        // Mock Request and Data
        public static List<Offer> FetchAvailableOffers() 
        {
            Product reward1 = new Product("currency", "coins");
            Product reward2 = new Product("game_item", "character_skin");

            ProductBundle rewardBundle1 = new ProductBundle(reward1, 200);
            ProductBundle rewardBundle2 = new ProductBundle(reward2, 1);

            OfferValidationData levelValidation = new OfferValidationData("min_user_level", "", 3);

            EventTrigger eventTrigger = new EventTrigger("main_menu_enter");

            return new List<Offer>()
            {
                new Offer("offer_1", 
                          "transaction1",
                          eventTrigger, 
                          levelValidation,
                          "offer_2"),
                new Offer("offer_2",
                          "transaction2",
                          eventTrigger,
                          levelValidation,
                          "offer_3"),
                new Offer("offer_3",
                          "transaction3",
                          eventTrigger,
                          levelValidation,
                          "offer_1"),
                new Offer("offer_4",
                         "transaction4",
                          new DateTrigger(DateTime.UtcNow + TimeSpan.FromDays(1), TimeSpan.FromDays(5)),
                          levelValidation),
            };
        }
    }
}