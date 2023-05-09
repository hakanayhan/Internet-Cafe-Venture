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
        SwitchState(new MoveState(this, managerDeskTransform, new IdleState(this)));
        onDesk = true;
    }

    public void AssignTable(CustomerStateMachine customer)
    {
        foreach(Computers computers in ComputerManager.Instance.computers)
        {
            if (computers.isIdle)
            {
                computers.isIdle = false;
                SwitchState(new AssignTableState(this, customer, computers.computer));
                break;
            }
        }
    }
}
