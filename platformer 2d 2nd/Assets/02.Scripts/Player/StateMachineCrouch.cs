using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineCrouch : StateMachineBase
{
    private CapsuleCollider2D _col;
    private Vector2 _colOffsetOrigin;
    private Vector2 _colSizeOrigin;
    private Vector2 _colOffsetCrouch = new Vector2(0.0f, 0.075f);
    private Vector2 _colSizeCrouch = new Vector2(0.15f, 0.15f);
    public StateMachineCrouch(StateMachineManager.State machineState,
                              StateMachineManager manager,
                              AnimationManager animationManager)
        : base(machineState, manager, animationManager)
    {
        _col = manager.GetComponent<CapsuleCollider2D>();
        _colOffsetOrigin = _col.offset;
        _colSizeOrigin = _col.size;
    }

    public override void Execute()
    {
        manager.isMovable = false;
        manager.isDirectionChangable = true;
        _col.offset = _colOffsetCrouch;
        _col.size = _colSizeCrouch;
        state = State.Prepare;
    }

    public override void FixedUpdateState()
    {
    }

    public override void ForceStop()
    {
        _col.offset = _colOffsetOrigin;
        _col.size = _colSizeOrigin;
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if (manager.state == StateMachineManager.State.Idle ||
            manager.state == StateMachineManager.State.Move)
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
                animationManager.Play("Crouch");
                state = State.OnAction;
                break;
            case State.Casting:
                break;
            case State.OnAction:
                if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    nextState = StateMachineManager.State.Idle;
                }
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
