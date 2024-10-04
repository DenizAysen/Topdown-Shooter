using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Keeps players basic properties
/// </summary>
[RequireComponent(typeof(Rigidbody),typeof(Animator))]
public class Player : MonoBehaviour
{
    #region Properties
    protected bool isControlEnabled;
    protected bool isPlayedDead;
    protected Rigidbody rb;
    protected Animator animator;
    #endregion

    #region Unity Methods   
    protected virtual void Awake()
    {
        isControlEnabled = false;
        isPlayedDead = false;
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerHealth.onPlayerDied += OnPlayerDied;
        PlayerCollector.onFinished += OnFinished;
    }
    private void OnDisable()
    {
        PlayerHealth.onPlayerDied -= OnPlayerDied;
        PlayerCollector.onFinished -= OnFinished;
    }
    #endregion
    #region Privates
    private void OnHit(float damage)
    {
        
    }
    private void OnStartGame()
    {
        isControlEnabled = true;
        isPlayedDead = false;
        animator.SetBool(CommonVariables.PlayerAnimBools.Run.ToString(), true);
    }
    private void OnPlayerDied()
    {
        isControlEnabled = false;
        isPlayedDead = true;
    }
    private void OnFinished()
    {
        isControlEnabled = false;
        //Debug.Log("Control enabled : " + isControlEnabled);
        animator.SetBool(CommonVariables.PlayerAnimBools.Run.ToString(), false);
    }
    #endregion
}
