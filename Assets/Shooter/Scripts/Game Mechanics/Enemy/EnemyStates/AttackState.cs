using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : IState
{
    private Animator _enemyAnimator;
    private NavMeshAgent _navMeshAgent;
    private EnemyBase _enemyBase;
    private Transform _playerTransform;
    private Rigidbody _rigidbody;
    public AttackState(Rigidbody rigidbody, Animator enemyAnimator, NavMeshAgent navMeshAgent,EnemyBase enemyBase, Transform playerTransform)
    {
        _rigidbody = rigidbody;
        _enemyAnimator = enemyAnimator;
        _navMeshAgent = navMeshAgent;
        _enemyBase = enemyBase;
        _playerTransform = playerTransform;
    }
    public void Enter()
    {
        Debug.Log("Entered attack state");
        _enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), true);
    }

    public void Exit()
    {
        _enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        if (Vector3.Distance(_enemyBase.transform.position, _playerTransform.position) > 10f)
        {
            _enemyBase.ChangeState(_enemyBase.ChaseState);
        }
        else
        {
            StopEnemyMove();
            _enemyBase.transform.LookAt(_playerTransform);
            _enemyBase.transform.eulerAngles = new Vector3(0,_enemyBase.transform.eulerAngles.y,0);
        }
    }
    #region Private Methods
    private void StopEnemyMove()
    {
        _navMeshAgent.ResetPath();
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
    #endregion
}
