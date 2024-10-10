using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
	#region Unity Fields
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float maxLifeTime = 10f;
    #endregion
    #region Fields
    private float _bulletDamage;
    private Transform _shooterObject;
    #endregion
    #region Unity Methods
    private void OnEnable()
    {
        StartCoroutine(DisableAfterLifeTime());
    }
    private void Update()
    {
        if (_shooterObject == null)
            return;

        transform.position += _shooterObject.forward * Time.fixedDeltaTime * .1f * bulletSpeed;
    }
    #endregion
    #region Private Methods
    private IEnumerator DisableAfterLifeTime()
    {
        yield return new WaitForSeconds(maxLifeTime);
        gameObject.SetActive(false);
    }
    #endregion
    #region Public Methods
    public void InitBullet(float bulletDamage, Transform shooterObject)
    {
        _bulletDamage = bulletDamage;
        _shooterObject = shooterObject;
    }
    #endregion
    #region On Trigger
    private void OnTriggerEnter(Collider other)
    {
        
    }
    #endregion
}
