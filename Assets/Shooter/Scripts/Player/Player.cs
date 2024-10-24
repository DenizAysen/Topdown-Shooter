using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonVariables;
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
        //animator = GetComponent<Animator>();
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerHealth.onPlayerDied += OnPlayerDied;
        GameManager.onGameFinished += OnGameFinished;
    }
    private void OnDisable()
    {
        PlayerHealth.onPlayerDied -= OnPlayerDied;
        GameManager.onGameFinished -= OnGameFinished;
    }
    #endregion
    #region Privates
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
        Debug.Log($"Control enabled state : {isControlEnabled}");
    }

    private void OnGameFinished()
    {
        isControlEnabled = false;
        animator.SetBool(PlayerAnimBools.Run.ToString(), false);
        animator.SetBool(PlayerAnimBools.Shooting.ToString(), false);
        animator.Play(PlayerAnimState.Idle.ToString());
    }

    #endregion
}
