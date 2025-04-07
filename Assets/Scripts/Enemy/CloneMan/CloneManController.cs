using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloneManController : EnemyController
    {
        private CloneManStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }
        public CloneManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            createStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }

        public void SetCloneCount(int cloneCount) => CloneCountLeft = cloneCount;

        private void createStateMachine() => stateMachine = new CloneManStateMachine(this);
        // Player enters range, change to CHASING state.
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }

        // Player exits range, change to IDLE state.
        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);


        public override void Die()
        {
            if (CloneCountLeft > 0)
                stateMachine.ChangeState(States.CLONE);
            base.Die();
        }

        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }



}
