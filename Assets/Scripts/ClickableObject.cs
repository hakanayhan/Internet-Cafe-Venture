using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject manager;
    private ManagerStateMachine managerState;
    public enum WhereTo { managerDesk, freeMove }
    public WhereTo whereTo;

    private Vector3 _firstClickPosition;
    private void Start()
    {
        managerState = manager.GetComponent<ManagerStateMachine>();
    }
    void OnMouseDown()
    {
        _firstClickPosition = Input.mousePosition;
    }
    void OnMouseUp()
    {
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
                if(clickedObject == this.gameObject)
                    managerState.MoveToFreePosition(hitPoint);
            }
        }
    }
}
