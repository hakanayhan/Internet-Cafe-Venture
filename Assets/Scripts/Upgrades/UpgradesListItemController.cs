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
    [SerializeField] private Text _priceText;
    double price;

    private void Start()
    {
        _icon.sprite = upgrade.icon;
        _title.text = upgrade.upgradeTitle;
        _description.text = upgrade.upgradeText;
        _functionText.text = GetFunctionText();
        price = upgrade.basePrice;
        _priceText.text = new Currency(price).ToString();
    }

    private string GetFunctionText()
    {
        if (upgrade.feature == Upgrade.Features.popularity)
        {
            string s = (upgrade.addRate > 1) ? "s" : "";
            return "+" + upgrade.addRate + " Customer" + s;
        }
        return null;
    }
}
