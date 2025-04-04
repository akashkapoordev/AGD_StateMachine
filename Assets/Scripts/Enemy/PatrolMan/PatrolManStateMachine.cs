using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;

public class PatrolManStateMachine : IStateMachine
{
    private PatrolManController Owner;
    private IState currentState;
    private Dictionary<States, IState> states = new Dictionary<States, IState>();


    public PatrolManStateMachine(PatrolManController Owner)
    {
        this.Owner = Owner;
        CreateState();
        SetOwner();

    }

    public void Update() => currentState?.Update();


    private void CreateState()
    {
        states.Add(States.IDLE, new IdleState(this));
        states.Add(States.ROTATING, new RotatingState(this));
        states.Add(States.SHOOTING, new ShootingState(this));
        states.Add(States.PATROLLING, new PatrollingState(this));
        states.Add(States.CHASING, new ChasingState(this));
    }

    private void SetOwner()
    {
        foreach(IState state in states.Values)
        {
            state.Owner = Owner;
        }
    }

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(States newState) => ChangeState(states[newState]);
}
