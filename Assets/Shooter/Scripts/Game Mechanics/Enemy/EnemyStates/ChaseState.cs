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
    private EnemyTypes enemyType;
    #endregion
    public ChaseState(Animator animator, NavMeshAgent navMeshAgent, Transform playerTransform, EnemyBase enemyBase, EnemyTypes enemyType)
    {
        enemyAnimator = animator;
        this.navMeshAgent = navMeshAgent;
        this.playerTransform = playerTransform;
        this.enemyBase = enemyBase;
        this.enemyType = enemyType;
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
        if (enemyBase.IsDead)
        {
            return;
        }

        if(Vector3.Distance(enemyBase.transform.position,playerTransform.position) < enemyType.ShootRange)
        {
            enemyBase.ChangeState(enemyBase.AttackState);
        }

        else
        {
            navMeshAgent.SetDestination(playerTransform.position);
        }
    }
}
