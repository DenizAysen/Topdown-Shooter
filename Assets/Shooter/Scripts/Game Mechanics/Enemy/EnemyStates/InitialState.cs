using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    #region Properties
    private StateMachineBase stateMachineBase;
    private Animator enemyAnimator; 
    #endregion
    public InitialState(StateMachineBase stateMachineBase, Animator animator)
    {
        this.stateMachineBase = stateMachineBase;
        enemyAnimator = animator;
    }
    public void Enter()
    {
        Debug.Log("Enemy is at initial state");
        //enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Idle.ToString(), true);
    }

    public void Exit()
    {

    }

    public void FixedTick()
    {

    }

    public void Tick()
    {

    }
}
