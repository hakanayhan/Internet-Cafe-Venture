using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject manager;
    private ManagerStateMachine managerState;
    public enum WhereTo { managerDesk, freeMove }
    public WhereTo whereTo;
    private void Start()
    {
        managerState = manager.GetComponent<ManagerStateMachine>();
    }
    void OnMouseUp()
    {
        if (whereTo == WhereTo.managerDesk)
            managerState.MoveToManagerDesk();

        if (whereTo == WhereTo.freeMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPoint = hit.point;
                managerState.MoveToFreePosition(hitPoint);
            }
        }
    }
}
