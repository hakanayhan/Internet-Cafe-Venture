using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesWindow : Window
{
    public static UpgradesWindow Instance;
    [SerializeField] GameObject panel;
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
