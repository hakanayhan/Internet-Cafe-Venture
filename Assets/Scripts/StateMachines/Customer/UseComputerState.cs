using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseComputerState : State
{
    CustomerStateMachine stateMachine;
    Computers computer;
    float usageTime;

    public UseComputerState(CustomerStateMachine stateMachine, Computers computer)
    {
        this.stateMachine = stateMachine;
        this.computer = computer;
        usageTime = stateMachine.usageTime;
    }

    public override void Enter()
    {
        stateMachine.radialTimer.StartTimer(usageTime);
    }

    public override void Tick(float deltaTime)
    {
        usageTime = Mathf.Max(0f, usageTime -= deltaTime);

        if (usageTime == 0f)
            stateMachine.SwitchState(new MoveState(stateMachine, CustomerManager.Instance.checkoutLocation, new PayAndLeaveState(stateMachine, computer)));
    }

    public override void Exit()
    {
    }
}
