using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Player, IDamageable
{
    #region Unity Fields
    [SerializeField] float maxHealth;
    [SerializeField] Image playerHealthImage;
    #endregion
    #region Fields
    float _currentHealth;
    private int _unusedObjectLayer = 6;
    #endregion
    public static Action onPlayerDied;
    public static Action onTakeDamage;
    #region Properties
    public bool IsDead { get; set; }
    public float CurrentHealth { get; set; } 
    #endregion
    #region Unity Methods
    protected override void Start()
    {
        base.Start();
        CurrentHealth = maxHealth;
        playerHealthImage.fillAmount = CurrentHealth / maxHealth;
    }
    #endregion
    #region Private Methods
    //private void OnHit(float damage)
    //{
    //    CurrentHealth -= damage;

    //    if (CurrentHealth <= 0)
    //    {
    //        animator.SetTrigger(CommonVariables.PlayerAnimBools.Die.ToString());
    //        isControlEnabled = false;
    //        IsDead = true;
    //        onPlayerDied?.Invoke();
    //    }
    //    else
    //        animator.SetTrigger(CommonVariables.PlayerAnimsTriggers.Hit.ToString());

    //}
    #endregion
    #region Public Methods
    public float GetMaxHealth() => maxHealth;
    public float GetCurrentHealth() => CurrentHealth;
    public void TakeDamage(float damage)
    {
        if (IsDead) return;

        CurrentHealth -= damage;
        playerHealthImage.fillAmount = CurrentHealth / maxHealth;
        if (CurrentHealth <= 0)
        {
            animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
            animator.SetBool(CommonVariables.PlayerAnimBools.Die.ToString(),true);
            IsDead = true;
            gameObject.layer = _unusedObjectLayer;
            onPlayerDied?.Invoke();
        }
        else
        {
            animator.SetTrigger(CommonVariables.PlayerAnimsTriggers.Hit.ToString());
            onTakeDamage?.Invoke();
        }
    }
    public bool TakeHealth(float healthValue)
    {
        if(CurrentHealth == maxHealth)
        {
            return false;
        }
        CurrentHealth += healthValue;
        playerHealthImage.fillAmount = CurrentHealth / maxHealth;
        return true;
    }
    #endregion
}
