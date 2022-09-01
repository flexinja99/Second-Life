using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineFall : StateMachineBase
{

    private GroundDetector _groundDetector;
    public StateMachineFall(StateMachineManager.State machineState,
                            StateMachineManager manager,
                            AnimationManager animationManager)
        : base(machineState, manager, animationManager)
    {
        _groundDetector = manager.GetComponent<GroundDetector>();
    }

    public override void Execute()
    {
        manager.isMovable = false;
        manager.isDirectionChangable = true;
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
        if (_groundDetector.isDetected == false &&
            (manager.state == StateMachineManager.State.Idle ||
             manager.state == StateMachineManager.State.Move ||
             manager.state == StateMachineManager.State.Jump))
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
                animationManager.Play("Fall");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if (_groundDetector.isDetected)
                {
                    state++;
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
