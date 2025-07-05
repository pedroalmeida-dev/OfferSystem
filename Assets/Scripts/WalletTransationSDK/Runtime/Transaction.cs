namespace WalletTransation
{
    public class Transaction
    {
        public string Id;
        public float Price;
        public Bundle Bundle;

        public Transaction(string id, float price, Bundle bundle) 
        { 
            Id = id;
            Price = price;
            Bundle = bundle;
        }
    }
}