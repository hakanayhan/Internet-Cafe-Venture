using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseComputerState : State
{
    CustomerStateMachine stateMachine;
    Computers computer;
    float usageTime;
    float initialUsageTime;

    int lastUsageTime;

    public UseComputerState(CustomerStateMachine stateMachine, Computers computer)
    {
        this.stateMachine = stateMachine;
        this.computer = computer;
        usageTime = stateMachine.usageTime;
        initialUsageTime = stateMachine.usageTime;
    }

    public override void Enter()
    {
        ComputerInfoWindow.Instance.Refresh();
        stateMachine.radialTimer.StartTimer(usageTime);
    }

    public override void Tick(float deltaTime)
    {
        usageTime = Mathf.Max(0f, usageTime -= deltaTime);
        int currentUsageTime = Mathf.FloorToInt(initialUsageTime - usageTime);
        if(currentUsageTime != lastUsageTime)
        {
            lastUsageTime = currentUsageTime;
            computer.usageTime = currentUsageTime;
            computer.totalCost += computer.cost;
            stateMachine.amountToPay += computer.cost;
            ComputerInfoWindow.Instance.Refresh();
        }
        
        if (usageTime == 0f)
            stateMachine.SwitchState(new MoveState(stateMachine, CustomerManager.Instance.checkoutLocation, new PayAndLeaveState(stateMachine, computer)));
            
    }

    public override void Exit()
    {
        computer.customer = null;
        ComputerInfoWindow.Instance.Refresh();
    }
}
