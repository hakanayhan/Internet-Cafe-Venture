using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateMachine : StateMachine
{
    public bool inQueue = false;
    public RadialTimer radialTimer;
    public float usageTime;
    public double amountToPay;
    public void QueueUp(Transform queuePos)
    {
        inQueue = true;
        SwitchState(new MoveState(this, queuePos, new IdleState(this)));
    }

    private void OnDestroy()
    {
        CustomerManager.Instance.SetDelayTime();
        CustomerManager.Instance.customers.Remove(this);
    }
}
