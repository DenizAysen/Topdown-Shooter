using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerShooter : Player //, IDamageable
{
    #region Unity Fields
    [SerializeField] private float playerMaxHealth = 100f;
    [SerializeField] private float bulletDamage = 40f;
    [SerializeField] private float playerShootTimeOut = 1f;
	[SerializeField] Image playerHealthImage;
	[SerializeField] Transform gunPoint;
    #endregion
    #region Fields
    TimeCounter _timeCounter;
    private bool _isShootPressed;
    #endregion
    //#region Properties
    //public bool IsDead { get; set; }
    //public float CurrentHealth { get; set; }
    //#endregion

    //#region Public Methods
    //public void TakeDamage(float damage)
    //{
    //    CurrentHealth -= damage;
    //    playerHealthImage.fillAmount = CurrentHealth / playerMaxHealth;
    //}
    //#endregion
    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        isControlEnabled = true;
        _timeCounter = new TimeCounter(playerShootTimeOut);
        ButtonHold.onPressedFire += SetShoot;
    }
    private void OnEnable()
    {
        ButtonHold.onPressedFire += SetShoot;
        PlayerHealth.onTakeDamage += OnTakeDamage;
        PlayerHealth.onPlayerDied += OnPlayerDied;
    }
    protected override void Start()
    {
        base.Start();
        //CurrentHealth = playerMaxHealth;
    }
    private void OnDisable()
    {
        ButtonHold.onPressedFire -= SetShoot;
        PlayerHealth.onTakeDamage -= OnTakeDamage;
        PlayerHealth.onPlayerDied -= OnPlayerDied;
    }
    private void Update()
    {
        if (!isActiveAndEnabled)
            return;

        if(_isShootPressed && _timeCounter.IsThickFinished(Time.deltaTime))
        {
            ShootBullet();
        }
    }
    #endregion
    #region Private Methods
    private void SetShoot(bool isShooting)
    {
        if (!isControlEnabled)
        {
            return;
        }

        if (!_isShootPressed && isShooting)
        {
            ShootBullet();

            _timeCounter = new TimeCounter(playerShootTimeOut);
        }
        _isShootPressed = isShooting;

        if (!_isShootPressed)
        {
            animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
        }

    }

    private void ShootBullet()
    {
        animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), true);
        GameObject bullet = CreateGameObjects.Instance.CreateGameObject("Bullet", gunPoint.position,null);
        bullet.GetComponent<BulletBase>().InitBullet(bulletDamage, gunPoint, transform);
    }
    private void OnTakeDamage() => _timeCounter = new TimeCounter(playerShootTimeOut);
    private void OnPlayerDied() => _isShootPressed  = false;
    #endregion
}
