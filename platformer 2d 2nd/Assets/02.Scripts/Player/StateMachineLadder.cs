using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineLadder : StateMachineBase
{
    private LadderDetector _ladderDetector;
    private GroundDetector _groundDetector;
    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    private float _speed = 2.0f;

    public StateMachineLadder(StateMachineManager.State machineState,
                              StateMachineManager manager,
                              AnimationManager animationManager)
        : base(machineState, manager, animationManager)
    {
        _ladderDetector = manager.GetComponent<LadderDetector>();
        _groundDetector = manager.GetComponent<GroundDetector>();
        _col = manager.GetComponent<CapsuleCollider2D>();
        _rb = manager.GetComponent<Rigidbody2D>();
    }

    public override void Execute()
    {
        Debug.LogWarning("Excute ladder state");
        manager.isMovable = false;
        manager.isDirectionChangable = false;
        animationManager.speed = 0.0f;
        _rb.bodyType = RigidbodyType2D.Kinematic;
        state = State.Prepare;
    }

    public override void FixedUpdateState()
    {
    }

    public override void ForceStop()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
        state = State.Idle;
    }

    public override bool IsExecuteOK()
    {
        bool isOK = false;
        if ((_ladderDetector.isGoUpPossible ||
             _ladderDetector.isGoDownPossible) &&
             (manager.state == StateMachineManager.State.Idle ||
              manager.state == StateMachineManager.State.Move ||
              manager.state == StateMachineManager.State.Jump ||
              manager.state == StateMachineManager.State.Fall ||
              manager.state == StateMachineManager.State.Dash))
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
                animationManager.Play("Ladder");

                if (_ladderDetector.isGoUpPossible)
                {
                    _rb.position = new Vector2(_ladderDetector.ladderBottomPoint.x,
                                               _rb.position.y + _col.size.y * 0.5f);
                }
                else if (_ladderDetector.isGoDownPossible)
                {
                    _rb.position = new Vector2(_ladderDetector.ladderTopPoint.x,
                                               _rb.position.y - _col.size.y * 0.5f);
                }

                //state = State.OnAction;
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                float v = Input.GetAxisRaw("Vertical");
                animationManager.speed = Mathf.Abs(v);
                _rb.MovePosition(_rb.position + Vector2.up * v * _speed * Time.deltaTime);
                //_rb.position += Vector2.up * v * Time.deltaTime;

                // 사다리 아래로 떨어지는 조건
                if (_rb.position.y + (_col.size.y / 2.0f) < _ladderDetector.ladderBottomPoint.y ||
                    (_rb.position.y < _ladderDetector.ladderBottomPoint.y && _groundDetector.isDetected))
                {
                    state = State.Finish;
                }
                // 사다리 위로 올라가는 조건
                else if (_rb.position.y + (_col.size.y / 4.0f) > _ladderDetector.ladderTopPoint.y)
                {
                    _rb.position = _ladderDetector.ladderTopPoint;
                    state = State.Finish;
                }

                // 점프
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    if (manager.h < 0)
                        manager.direction = -1;
                    else if (manager.h > 0)
                        manager.direction = 1;

                    manager.move.x = manager.h;
                    manager.ForceChangeState(StateMachineManager.State.Jump);
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

        Debug.LogWarning($"ladder machine : {state}");
        return nextState;
    }
}
