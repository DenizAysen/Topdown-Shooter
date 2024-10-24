using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    #region Properties
    private EnemyBase _enemyBase;
    private EnemyTypes _enemyType;
    private Animator enemyAnimator;
    private Transform _playerTransform;
    #endregion
    public InitialState(EnemyBase enemyBase, EnemyTypes enemyType, Animator animator, Transform playerTransform)
    {
        _enemyBase = enemyBase;
        _enemyType = enemyType;
        enemyAnimator = animator;
        _playerTransform = playerTransform;
    }
    public void Enter()
    {
        //Debug.Log("Enemy is at initial state");
        //enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Idle.ToString(), true);
        enemyAnimator.Play("Idle");
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {

    }

    public void Tick()
    {
        if(Vector3.Distance(_playerTransform.position, _enemyBase.transform.position) < _enemyType.ChaseRange)
        {
            _enemyBase.ChangeState(_enemyBase.ChaseState);
        }
    }
}
