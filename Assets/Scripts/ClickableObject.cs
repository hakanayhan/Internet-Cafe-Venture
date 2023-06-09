using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject manager;
    private ManagerStateMachine managerState;
    public enum WhereTo { Null, managerDesk, freeMove }
    public WhereTo whereTo;

    public enum Windows { Null, computerInfoWindow, computerUnlockWindow, upgradesWindow }
    public Windows openWindow;

    public GameObject referenceGameObject;

    private Vector3 _firstClickPosition;

    void OnMouseDown()
    {
        _firstClickPosition = Input.mousePosition;
    }
    void OnMouseUp()
    {
        if (whereTo != WhereTo.Null && !CloseWindowsOnClick.Instance.windowOpened)
            MoveToDestination();
        if (openWindow != Windows.Null)
            OpenWindow();
    }

    private void MoveToDestination()
    {
        managerState = manager.GetComponent<ManagerStateMachine>();
        Vector3 currentClickPosition = Input.mousePosition;
        if ((_firstClickPosition - currentClickPosition).magnitude > 10f)
            return;

        if (whereTo == WhereTo.managerDesk)
            managerState.MoveToManagerDesk();

        if (whereTo == WhereTo.freeMove)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 hitPoint = hit.point;
                GameObject clickedObject = hit.collider.gameObject;
                if (clickedObject == this.gameObject)
                    managerState.MoveToFreePosition(hitPoint);
            }
        }
    }

    private void OpenWindow()
    {
        if (openWindow == Windows.computerInfoWindow)
            ComputerInfoWindow.Instance.OpenWindow(referenceGameObject.GetComponent<Computer>());

        if (openWindow == Windows.computerUnlockWindow)
            ComputerUnlockWindow.Instance.OpenWindow(referenceGameObject.GetComponent<Computer>());

        if (openWindow == Windows.upgradesWindow)
            UpgradesWindow.Instance.OpenWindow();
    }
}
