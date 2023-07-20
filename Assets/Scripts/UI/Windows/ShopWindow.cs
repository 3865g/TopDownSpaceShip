using TMPro;

namespace Scripts.UI.Windows
{
    public class ShopWindow: WindowBase
    {
        public TextMeshProUGUI CoinText;

        protected override void Initialize()
        {
            RefreshCoinText();
        }
        protected override void SubscribeUpdates()
        {
            PlayerProgress.WorldData.LootData.ChangedValue += RefreshCoinText;
        }

        protected override void Clenup()
        {
            base.Clenup();
            PlayerProgress.WorldData.LootData.ChangedValue -= RefreshCoinText;
        }

        private void RefreshCoinText()
        {
             CoinText.text = PlayerProgress.WorldData.LootData.Collected.ToString();
        }
    }
}
