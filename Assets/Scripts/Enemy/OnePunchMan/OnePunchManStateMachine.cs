using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : IStateMachine
    {
        private OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<States, IState> EnemyState = new Dictionary<States, IState>();

        public OnePunchManStateMachine(OnePunchManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            EnemyState.Add(States.IDLE, new IdleState(this));
            EnemyState.Add(States.ROTATING, new RotatingState(this));
            EnemyState.Add(States.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach(IState state in EnemyState.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(States newState) => ChangeState(EnemyState[newState]);
    }

  
}