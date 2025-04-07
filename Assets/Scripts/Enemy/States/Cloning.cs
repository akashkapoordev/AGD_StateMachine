using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

public class Cloning<T> : IState where T : EnemyController
{
    public EnemyController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;


    public Cloning(GenericStateMachine<T> stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void OnStateEnter()
    {
        createClone();
        createClone();
    }

    public void OnStateExit()
    {
        
    }

    public void Update()
    {
    }

    private void createClone()
    {
        CloneManController cloneMan = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as CloneManController;
        cloneMan.SetCloneCount((Owner as CloneManController).CloneCountLeft - 1);
        cloneMan.Teleport();
        cloneMan.SetDefaultColor(EnemyColorType.Clone);
        cloneMan.ChangeColor(EnemyColorType.Clone);
        GameService.Instance.EnemyService.AddEnemy(cloneMan);
    }
  
}
