using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerStateMachine : StateMachine
{
    public QueueController queueController;
    void Start()
    {
        queueController = FindObjectOfType<QueueController>();
        AssignManager();
    }

    public void AssignManager()
    {
        SwitchState(new MoveState(this, queueController.customerPos, new IdleState(this)));
    }
}
