using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.StateMachine;
using UnityEngine;

public class GenericStateMachine<T> where T : EnemyController
{
    protected T Owner;
    protected IState currentState;
    protected Dictionary<States,IState> States = new Dictionary<States,IState>();

    public GenericStateMachine(T owner)
    {
        this.Owner = owner;
    }

    protected void Update() => currentState?.Update(); 

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(States newState) => ChangeState(States[newState]);

    protected void SetOwner()
    {
        foreach(IState state in States.Values)
        {
            state.Owner = Owner;
        }
    }
}
