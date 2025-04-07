using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class TeleportingState<T> : IState where T : EnemyController
{
    public EnemyController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;

    public TeleportingState(GenericStateMachine<T> stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        TeleportToRandomPosition();
        stateMachine.ChangeState(States.CHASING);
    }

    public void OnStateExit()
    {
       
    }

    public void Update()
    {
       
    }

    private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPoint());
    private Vector3 GetRandomNavMeshPoint()
    {
        Vector3 randomDirection = Random.insideUnitSphere * 5f + Owner.Position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
            return hit.position;

        return Owner.Data.SpawnPosition;
    }
}
