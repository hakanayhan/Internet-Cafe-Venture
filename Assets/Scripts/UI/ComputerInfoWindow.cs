using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerInfoWindow : Window
{
    public static ComputerInfoWindow Instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text deviceNameText;
    [SerializeField] Text levelText;
    [SerializeField] Text costPerSecText;
    [SerializeField] Text customerText;
    [SerializeField] Text usageTimeText;
    [SerializeField] Text totalCostText;

    Computers computer;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void LoadData(Computer computerObject)
    {
        Computers computer = ComputerManager.Instance.computers.Find(a => a.computerObject == computerObject);
        LoadData(computer);
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
        float level = computer.level;
        levelText.text = level.ToString();
        Currency cost = new Currency(computer.cost);
        costPerSecText.text = cost.ToString();
        string customerName = (computer.customer != null) ? computer.customer.customerName : "Empty";
        customerText.text = customerName;
        float usageTime = computer.usageTime;
        string s = usageTime >= 2 ? " secs" : " sec";
        usageTimeText.text = usageTime.ToString() + s;
        totalCostText.text = computer.totalCost.ToString();
    }

    public void OpenWindow(Computer computerObject)
    {
        LoadData(computerObject);
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }
    public override void CloseWindow()
    {
        panel.SetActive(false);
    }
}
