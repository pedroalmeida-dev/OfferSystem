using UnityEngine;

namespace WalletTransation
{
    public class ProductBundle : MonoBehaviour
    {
        public Product Reward;
        public uint Amount;

        public ProductBundle(Product reward, uint amount) 
        { 
            Reward = reward;
            Amount = amount;
        }
    }
}