using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerShooter : Player, IDamageable
{
    #region Unity Fields
    [SerializeField] private float playerMaxHealth = 100f;
    [SerializeField] private float playerShootTimeOut = 1f;
	[SerializeField] Image playerHealthImage;
	[SerializeField] Transform gunPoint;
    #endregion
    #region Fields
    TimeCounter _timeCounter;
    #endregion
    #region Properties
    public bool IsDead { get; set; }
    public float CurrentHealth { get; set; }
    #endregion

    #region Public Methods
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        playerHealthImage.fillAmount = CurrentHealth / playerMaxHealth;
    }
    #endregion
    #region Unity Methods
    private void Awake()
    {
        _timeCounter = new TimeCounter(playerShootTimeOut);
    }
    private void Start()
    {
        CurrentHealth = playerMaxHealth;
    }
    #endregion
}
