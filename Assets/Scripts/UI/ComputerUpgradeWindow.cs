using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerUpgradeWindow : Window
{
    [SerializeField] GameObject panel;
    Computers computer;
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
