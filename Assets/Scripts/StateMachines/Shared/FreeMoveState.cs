using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeMoveState : State
{
    StateMachine stateMachine;

    Vector3 destination;

    public FreeMoveState(StateMachine stateMachine, Vector3 destination)
    {
        this.stateMachine = stateMachine;
        this.destination = destination;
    }

    public override void Enter()
    {
        stateMachine.agent.enabled = true;
        stateMachine.agent.SetDestination(destination);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.agent.pathPending)
            return;

        if (stateMachine.agent.remainingDistance < 0.1f)
        {
            stateMachine.SwitchState(new IdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.agent.enabled = false;
    }
}