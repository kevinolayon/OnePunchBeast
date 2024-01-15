using DG.Tweening;
using TMPro;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField] int currency;
    [SerializeField] int amountReceived;
    [SerializeField] int colorUpgradePrice = 100;
    [SerializeField] int stackUpgradePrice = 150;
    [SerializeField] CanvasGroup upgrades;
    [SerializeField] TextMeshProUGUI currencyText;
    [SerializeField] TextMeshProUGUI stackPriceText;
    [SerializeField] TextMeshProUGUI colorPriceText;
    [SerializeField] TextMeshProUGUI maxStackCount;

    PlayerManager player;

    private void Awake()
    {
        player = PlayerManager.Instance;
        currencyText.text = currency.ToString("C");
        stackPriceText.text = stackUpgradePrice.ToString("C");
        colorPriceText.text = colorUpgradePrice.ToString("C");
    }

    private void Start()
    {
        maxStackCount.text = player.CurrentStack() + " / " + player.MaxStack();
    }

    public void ShowUpgrades()
    {
        upgrades.DOFade(1, .25f);
        upgrades.interactable = true;
        upgrades.blocksRaycasts = true;
    }

    public void HideUpgrades()
    {
        upgrades.DOFade(0, .25f);
        upgrades.interactable = false;
        upgrades.blocksRaycasts = false;
    }

    public void AddCurrency(int amount)
    {
        currency += amount * amountReceived;
        currencyText.text = currency.ToString("C");
    }

    public void RemoveCurrency(int amount)
    {
        currency -= amount;
        if (currency < 0) currency = 0;
        currencyText.text = currency.ToString("C");
    }

    public void UpgradeStack()
    {
        if (currency < stackUpgradePrice) return;
        player.IncreaseStack();
        RemoveCurrency(stackUpgradePrice);
        UpdateStack();
    }

    public void UpgradeColor()
    {
        if (currency < colorUpgradePrice) return;
        player.ChangeColor();
        RemoveCurrency(colorUpgradePrice);
    }

    public void UpdateStack()
    {
        maxStackCount.text = player.CurrentStack() + " / " + player.MaxStack();
    }
}
