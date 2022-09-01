using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineDie : StateMachineBase
{
    public StateMachineDie(StateMachineManager.State machineState,
                          StateMachineManager manager,
                          AnimationManager animationManager)
       : base(machineState, manager, animationManager)
    {
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
        return true;
    }

    public override StateMachineManager.State UpdateState()
    {
        StateMachineManager.State nextState = managerState;

        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                animationManager.Play("Die");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                // nothing to do
                break;
            case State.Finish:
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
