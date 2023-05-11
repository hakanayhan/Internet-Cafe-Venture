using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerInfoWindow : Window
{
    public static ComputerInfoWindow Instance;
    [SerializeField] GameObject panel;
    [SerializeField] Text levelText;
    [SerializeField] Text costPerSecText;
    [SerializeField] Text inUseText;
    [SerializeField] Text usageTimeText;
    [SerializeField] Text totalCostText;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void OpenWindow()
    {
        panel.SetActive(true);
        CloseWindowsOnClick.Instance.WindowOpened();
    }
    public override void CloseWindow()
    {
        panel.SetActive(false);
    }
}
