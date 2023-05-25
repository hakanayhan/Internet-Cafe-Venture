using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStateMachine : StateMachine
{
    public Transform managerDeskTransform;
    public bool onDesk;
    public float takeCustomerTimer = 3f;
    public RadialTimer radialTimer;
    public void Move(Transform movePos)
    {
        SwitchState(new MoveState(this, movePos, new IdleState(this)));
    }

    public void MoveToManagerDesk()
    {
        if (!onDesk)
        {
            ResetPositions();
            SwitchState(new MoveState(this, managerDeskTransform, new IdleState(this)));
            onDesk = true;
        }
    }

    public void MoveToFreePosition(Vector3 hit)
    {
        ResetPositions();
        SwitchState(new FreeMoveState(this, hit));
    }

    public void AssignTable(CustomerStateMachine customer)
    {
        foreach(Computers computer in ComputerManager.Instance.computers)
        {
            if (computer.isIdle && computer.level > 0)
            {
                SwitchState(new AssignTableState(this, customer, computer));
                break;
            }
        }
    }

    void ResetPositions()
    {
        onDesk = false;
    }
}
