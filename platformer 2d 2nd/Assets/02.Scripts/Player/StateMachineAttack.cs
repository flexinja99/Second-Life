using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineAttack : StateMachineBase
{
    private float _animationTime;
    private float _animationTimer;
    public StateMachineAttack(StateMachineManager.State machineState,
                              StateMachineManager manager,
                              AnimationManager animationManager)
        : base(machineState, manager, animationManager)
    {
        shortKey = KeyCode.A;
        _animationTime = animationManager.GetAnimationTime("Attack");
    }

    public override void Execute()
    {
        manager.isMovable = false;
        manager.isDirectionChangable = false;
        state = State.Prepare;
    }

    public override void FixedUpdateState()
    {
    }

    public override void ForceStop()
    {
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (manager.state == StateMachineManager.State.Idle ||
            manager.state == StateMachineManager.State.Move ||
            manager.state == StateMachineManager.State.Jump ||
            manager.state == StateMachineManager.State.Fall)
            isOK = true;
        return isOK;
    }

    public override StateMachineManager.State UpdateState()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                manager.ResetVelocity();
                animationManager.Play("Attack");
                _animationTimer = _animationTime;
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if (_animationTimer < 0)
                {
                    state = State.Finish;
                }
                _animationTimer -= Time.deltaTime;
                break;
            case State.Finish:
                nextState = StateMachineManager.State.Idle;
                break;
            case State.Error:
                break;
            case State.WaitForErrorClear:
                break;
            default:
                break;
        }

        return nextState;
    }
}
