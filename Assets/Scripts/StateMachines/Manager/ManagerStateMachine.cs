using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerStateMachine : StateMachine
{
    public Transform managerDeskTransform;
    public bool onDesk;
    public Transform computerTransform;
    public float takeCustomerTimer = 3f;
    public void Move(Transform movePos)
    {
        SwitchState(new MoveState(this, movePos, new IdleState(this)));
    }

    public void MoveToManagerDesk()
    {
        SwitchState(new MoveState(this, managerDeskTransform, new IdleState(this)));
        onDesk = true;
    }

    public void PlaceCustomer(CustomerStateMachine customer)
    {
        SwitchState(new AssignTableState(this, customer));
    }
}
