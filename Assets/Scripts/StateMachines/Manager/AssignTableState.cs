using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTableState : State
{
    ManagerStateMachine stateMachine;
    CustomerStateMachine customer;
    Computers computer;
    float takeCustomerTimer;

    public AssignTableState(ManagerStateMachine stateMachine, CustomerStateMachine customer, Computers computer)
    {
        this.stateMachine = stateMachine;
        this.customer = customer;
        this.computer = computer;
        takeCustomerTimer = stateMachine.takeCustomerTimer;
    }

    public override void Enter()
    {
        stateMachine.radialTimer.StartTimer(takeCustomerTimer);
    }

    public override void Tick(float deltaTime)
    {
        takeCustomerTimer = Mathf.Max(0f, takeCustomerTimer -= deltaTime);

        if (takeCustomerTimer == 0f)
            stateMachine.SwitchState(new IdleState(stateMachine));
    }

    public override void Exit()
    {
        QueueController.Instance.RemoveQueue(customer);
        customer.SwitchState(new MoveState(customer, computer.computerObject.customerPos, new UseComputerState(customer, computer)));
    }
}
