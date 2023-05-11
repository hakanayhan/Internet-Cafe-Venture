using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowsOnClick : MonoBehaviour
{
    public static CloseWindowsOnClick Instance;
    [HideInInspector] public bool windowOpened;
    [SerializeField] GameObject button;
    [SerializeField] Window[] windows;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void WindowOpened()
    {
        windowOpened = true;
        button.SetActive(true);
    }

    public void CloseAllWindows()
    {
        windowOpened = false;
        foreach (Window window in windows)
            window.CloseWindow();

        button.SetActive(false);
    }
}
