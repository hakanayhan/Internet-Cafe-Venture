using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject manager;
    private ManagerStateMachine managerState;
    public enum WhereTo { managerDesk }
    public WhereTo whereTo;
    private void Start()
    {
        managerState = manager.GetComponent<ManagerStateMachine>();
    }
    void OnMouseUp()
    {
        if (whereTo == WhereTo.managerDesk)
            managerState.MoveToManagerDesk();
    }
}
