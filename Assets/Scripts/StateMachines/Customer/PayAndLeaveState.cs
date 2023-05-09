using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayAndLeaveState : State
{
    CustomerStateMachine stateMachine;
    Computers computer;

    public PayAndLeaveState(CustomerStateMachine stateMachine, Computers computer)
    {
        this.stateMachine = stateMachine;
        this.computer = computer;
    }

    public override void Enter()
    {
        //payment
        computer.isIdle = true;
        stateMachine.SwitchState(new MoveState(stateMachine, CustomerManager.Instance.exitPoint, new DestroyState(stateMachine)));
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
    }
}
