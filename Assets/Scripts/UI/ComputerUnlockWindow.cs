using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUnlockWindow : Window
{
    public static ComputerUnlockWindow Instance;
    Computers computer;
    [SerializeField] GameObject panel;
    [SerializeField] Text deviceText;
    [SerializeField] Text buttonText;
    double unlockCost;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OpenWindow(Computer computerObject)
    {
        LoadData(computerObject);
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }

    public void LoadData(Computer computerObject)
    {
        computer = ComputerManager.Instance.computers.Find(a => a.computerObject == computerObject);
        unlockCost = computer.unlockCost;
        int index = ComputerManager.Instance.computers.IndexOf(computer) + 1;
        deviceText.text = "Computer " + index;
        buttonText.text = new Currency(unlockCost).ToShortString();
    }

    public void UnlockButton()
    {
        if (computer.level == 0 && Wallet.Instance.TryRemoveMoney(unlockCost))
        {
            computer.level++;
            computer.computerObject.GetComponent<Computer>().unlockGameObject.SetActive(false);
            computer.computerObject.GetComponent<Computer>().computerGameObject.SetActive(true);
            CloseWindowsOnClick.Instance.CloseAllWindows();
        }
    }

    public override void CloseWindow()
    {
        panel.SetActive(false);
    }
}
