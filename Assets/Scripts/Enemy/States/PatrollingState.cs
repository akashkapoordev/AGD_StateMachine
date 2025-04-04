using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrollingState : IState
    {
        public EnemyController Owner { get; set; }
        private IStateMachine stateMachine;
        private int currentIndexPoint = -1;
        private Vector3 destination;

        public PatrollingState(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }



        public void OnStateEnter()
        {
            SetNextWayPoint();
            destination = GetDesination();
            MoveTowardsDestination();
        }



        public void Update()
        {
            if (ReachedDestination)
            {
                stateMachine.ChangeState(States.IDLE);
            }
        }

        public void OnStateExit()
        {
            throw new System.NotImplementedException();
        }
        private void SetNextWayPoint()
        {
            if (currentIndexPoint == Owner.Data.PatrollingPoints.Count - 1)
            {
                currentIndexPoint = 0;
            }
            else
            {
                currentIndexPoint++;
            }
        }

        private Vector3 GetDesination()
        {
            return Owner.Data.PatrollingPoints[currentIndexPoint];
        }

        private void MoveTowardsDestination()
        {
            Owner.Agent.isStopped = false;
            Owner.Agent.SetDestination(destination);
        }

        private bool ReachedDestination => Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance;
    }

}
