using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUpgradeWindow : Window
{
    [SerializeField] GameObject panel;
    [SerializeField] Text deviceNameText;
    [SerializeField] Text levelText;
    [SerializeField] Text costPerSecText;
    [SerializeField] Text upgradeCostText;
    Computers computer;
    float level;
    Currency cost;
    Currency upgradeCost;
    public static ComputerUpgradeWindow Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadData(Computers computer)
    {
        this.computer = computer;
        Refresh();
    }

    public void Refresh()
    {
        int index = ComputerManager.Instance.computers.IndexOf(computer) + 1;
        string name = "Computer " + index;
        deviceNameText.text = name;
        level = computer.level;
        levelText.text = "Level " + level.ToString();
        cost = new Currency(computer.cost);
        costPerSecText.text = cost.ToString() + "/sec";
        upgradeCost = new Currency(computer.upgradeCost);
        upgradeCostText.text = upgradeCost.ToString();
    }

    public void LvUpButton()
    {
        if (Wallet.Instance.TryRemoveMoney(upgradeCost))
        {
            ComputerManager.Instance.LvUp(computer);
            Refresh();
        }
    }

    public void OpenWindow(Computers computer)
    {
        LoadData(computer);
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
    }
}
