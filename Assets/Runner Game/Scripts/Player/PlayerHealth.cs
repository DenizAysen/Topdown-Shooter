using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Player
{
    #region Unity Fields
    [SerializeField] float maxHealth;
    #endregion
    #region Fields
    float _currentHealth;
    #endregion
    public static Action onPlayerDied;
    #region Unity Methods
    private void OnEnable()
    {

    }
    private void OnDisable()
    {

    }
    protected override void Start()
    {
        base.Start();
        _currentHealth = maxHealth;
    }
    #endregion
    #region Private Methods
    private void OnHit(float damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            animator.SetTrigger(CommonVariables.PlayerAnimsTriggers.Die.ToString());
            isControlEnabled = false;
            isPlayedDead = true;
            onPlayerDied?.Invoke();
        }
        else
            animator.SetTrigger(CommonVariables.PlayerAnimsTriggers.Hit.ToString());

    }
    #endregion
    #region Public Methods
    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => _currentHealth;
    #endregion
}
