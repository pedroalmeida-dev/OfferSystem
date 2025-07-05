using System.Collections.Generic;
using System.Linq;

namespace WalletTransation
{
    public class Bundle
    {       
        public List<ProductBundle> RewardBundles = new List<ProductBundle>();

        public Bundle(params ProductBundle[] rewardBundle)
        {
            RewardBundles = rewardBundle.ToList();
        }
    }
}