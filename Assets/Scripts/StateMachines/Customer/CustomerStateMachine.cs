using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateMachine : StateMachine
{
    public bool inQueue = false;
    public void QueueUp(Transform queuePos)
    {
        inQueue = true;
        SwitchState(new MoveState(this, queuePos, new IdleState(this)));
    }
}
