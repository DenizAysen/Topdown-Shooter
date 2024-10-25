using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class PlayerShooter : Player //, IDamageable
{
    #region Unity Fields
    [SerializeField] private float bulletDamage = 40f;
    [SerializeField] private float playerShootTimeOut = 1f;
	[SerializeField] Image playerHealthImage;
	[SerializeField] Transform gunPoint;
    [SerializeField] List<PlayerGunUpgrade> gunUpgrades;
    #endregion
    #region Fields
    TimeCounter _timeCounter;
    private bool _isShootPressed;
    #endregion
    public int CurrentGunLevel { get; private set; }
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
        CurrentGunLevel = 1;
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        ButtonHold.onPressedFire += SetShoot;
        PlayerHealth.onTakeDamage += OnTakeDamage;
        PlayerHealth.onPlayerDied += OnPlayerDied;
        GameManager.onGameFinished += OnGameFinished;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        ButtonHold.onPressedFire -= SetShoot;
        PlayerHealth.onTakeDamage -= OnTakeDamage;
        PlayerHealth.onPlayerDied -= OnPlayerDied;
        GameManager.onGameFinished -= OnGameFinished;
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
            PlayShootAnimation();

            _timeCounter = new TimeCounter(playerShootTimeOut);
        }
        _isShootPressed = isShooting;

        if (!_isShootPressed)
        {
            animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
        }

    }
    private void PlayShootAnimation() => animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), true);
    private void ShootBullet()
    {       
        var currentGunLevelDetails = gunUpgrades.Where(x => x.Gunlevel == CurrentGunLevel).FirstOrDefault();
        Shoot(currentGunLevelDetails.shootPoints);
    }
    private void Shoot(Transform[] shootPoints)
    {
        foreach (var item in shootPoints)
        {
            GameObject bullet = CreateGameObjects.Instance.CreateGameObject(CommonVariables.SpawnedObjects.Bullet.ToString(), item.position, null);
            bullet.GetComponent<BulletBase>().InitBullet(bulletDamage, item, transform);
        }
    }
    private void OnTakeDamage() => _timeCounter = new TimeCounter(playerShootTimeOut);
    private void OnPlayerDied() => _isShootPressed  = false;
    private void OnGameFinished()
    {
        _isShootPressed = false;
        //animator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
    }

    #endregion
    #region Public Methods
    public bool TakeGunUpgrade()
    {
        if(CurrentGunLevel < 3)
        {
            CurrentGunLevel++;
            return true;
        }
        return false;
    }
    #endregion
}
