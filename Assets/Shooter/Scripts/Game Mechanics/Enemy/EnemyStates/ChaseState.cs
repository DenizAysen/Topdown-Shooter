using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : IState
{
    #region Properties
    private Animator enemyAnimator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private EnemyBase enemyBase;
    #endregion
    public ChaseState(Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform, EnemyBase enemyBase)
    {
        enemyAnimator = animator;
        this.navMeshAgent = navMeshAgent;
        this.playerTransform = playerTransform;
        this.enemyBase = enemyBase;
    }
    public void Enter()
    {
        Debug.Log("Enemy is at chase state");
        enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Run.ToString(), true);
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {

    }

    public void Tick()
    {
        if(Vector3.Distance(enemyBase.transform.position,playerTransform.position) < 10f)
        {
            enemyBase.ChangeState(enemyBase.AttackState);
        }
        else
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }
}
