using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.Player;
using UnityEngine;

public class PatrolManController : EnemyController
{
    private PatrolManStateMachine stateMachine;
    public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
    {
        enemyView.SetController(this);
        CreateStateMachine();
    }

    private void CreateStateMachine() => new PatrolManStateMachine(this);

    public override void UpdateEnemy()
    {
        if (currentState == EnemyState.DEACTIVE)
            return;

        stateMachine.Update();
    }

    // Called when a player enters this enemy's detection range.
    public override void PlayerEnteredRange(PlayerController targetToSet)
    {
        base.PlayerEnteredRange(targetToSet);
        stateMachine.ChangeState(States.CHASING);
    }

    // Called when a player exits this enemy's detection range.
    public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);
}
