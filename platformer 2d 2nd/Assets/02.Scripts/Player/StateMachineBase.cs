using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineBase 
{
    public KeyCode shortKey;
    protected enum State
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish,
        Error,
        WaitForErrorClear
    }
    protected State state { get; set; }
    protected StateMachineManager.State managerState { get; set; }
    protected StateMachineManager manager { get; set; }
    protected AnimationManager animationManager { get; set; }
    public StateMachineBase(StateMachineManager.State machineState,
                            StateMachineManager manager,
                            AnimationManager animationManager)
    {
        this.managerState = machineState;
        this.manager = manager;
        this.animationManager = animationManager;
    }

    public abstract bool IsExecuteOK();
    public abstract void Execute();
    public abstract void ForceStop();
    public abstract StateMachineManager.State UpdateState();
    public abstract void FixedUpdateState();
}
