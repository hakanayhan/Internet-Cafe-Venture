using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyState : State
{
    StateMachine stateMachine;

    public DestroyState(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
        stateMachine.Destroy();
    }

    public override void Tick(float deltaTime)
    {

    }

    public override void Exit()
    {
    }
}
