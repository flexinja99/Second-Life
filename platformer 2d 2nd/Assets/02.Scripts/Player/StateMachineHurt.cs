using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineHurt : StateMachineBase
{
    private float _animationTime;
    private float _animationTimer;
    public StateMachineHurt(StateMachineManager.State machineState,
                            StateMachineManager manager,
                            AnimationManager animationManager)
        : base(machineState, manager, animationManager)
    {
        _animationTime = animationManager.GetAnimationTime("Hurt");
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
        if (managerState != StateMachineManager.State.Attack &&
            managerState != StateMachineManager.State.Die)
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
                _animationTimer = _animationTime;
                animationManager.Play("Hurt");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if (_animationTimer < 0)
                {
                    state = State.Finish;
                }
                else
                {
                    _animationTimer -= Time.deltaTime;
                }
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
