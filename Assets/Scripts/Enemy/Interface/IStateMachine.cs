using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.Enemy
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);

    }

}
