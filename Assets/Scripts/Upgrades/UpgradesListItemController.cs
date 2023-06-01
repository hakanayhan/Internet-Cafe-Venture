using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesListItemController : MonoBehaviour
{
    public Upgrade upgrade;
    public Button upgradeButton;
    [SerializeField] private Image _icon;
    [SerializeField] private Text _title;
    [SerializeField] private Text _description;
    [SerializeField] private Text _functionText;
    [SerializeField] private Text _function2Text;
    [SerializeField] private Text _priceText;
    [SerializeField] private Text _levelText;
    double price;

    private void Start()
    {
        _icon.sprite = upgrade.icon;
        _title.text = upgrade.upgradeTitle;
        _description.text = upgrade.upgradeText;
        _functionText.text = GetFunctionText();
        _function2Text.text = GetFunction2Text();
        price = upgrade.basePrice;
        _priceText.text = new Currency(price).ToString();
        _levelText.text = "LVL " + upgrade.level.ToString();
    }

    private string GetFunctionText()
    {
        if (upgrade.feature == Upgrade.Features.popularity)
        {
            float i = CustomerManager.Instance.maxCustomer;
            if (upgrade.upgradeRank[upgrade.rank].rankUpLevel == upgrade.level + 1)
                i++;

            return "Max Customer: " + CustomerManager.Instance.maxCustomer + " => " + i;
        }
        return null;
    }

    private string GetFunction2Text()
    {
        if (upgrade.feature == Upgrade.Features.popularity)
        {
            double minSpawn = CustomerManager.Instance.minSpawnDelay;
            double maxSpawn = CustomerManager.Instance.maxSpawnDelay;
            double minSpawnUpgraded = minSpawn - 0.1;
            double maxSpawnUpgraded = maxSpawn - 0.2;

            string s = minSpawnUpgraded + "-" + maxSpawnUpgraded + "s";
            return "Spawn Delay: " + minSpawn + "-" + maxSpawn + "s => " + s;
        }
        return null;
    }

    public void UpgradeButton()
    {
        if (Wallet.Instance.TryRemoveMoney(price))
        {
            if (upgrade.feature == Upgrade.Features.popularity)
            {
                CustomerManager.Instance.minSpawnDelay = Math.Round(CustomerManager.Instance.minSpawnDelay - 0.1, 1);
                CustomerManager.Instance.maxSpawnDelay = Math.Round(CustomerManager.Instance.maxSpawnDelay - 0.2, 1);
                if (upgrade.upgradeRank[upgrade.rank].rankUpLevel == upgrade.level + 1)
                {
                    upgrade.rank++;
                    CustomerManager.Instance.maxCustomer += 1;
                }
            }
            upgrade.level++;
            price *= upgrade.priceMultiply;
            Reload();
        }
    }

    void Reload()
    {
        _priceText.text = new Currency(price).ToString();
        _levelText.text = "LVL " + upgrade.level.ToString();
        _functionText.text = GetFunctionText();
        _function2Text.text = GetFunction2Text();
    }
}
