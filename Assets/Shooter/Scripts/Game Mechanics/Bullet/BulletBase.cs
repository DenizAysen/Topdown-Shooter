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

    private Vector3 _shootPoint;
    private Transform _sourceObject;
    #endregion
    #region Unity Methods
    private void OnEnable()
    {
        StartCoroutine(DisableAfterLifeTime());
    }
    private void Update()
    {
        if (_shootPoint == null)
            return;

        transform.position += _shootPoint * Time.deltaTime * bulletSpeed;
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
    public void InitBullet(float bulletDamage, Transform gunPoint, Transform sourceObject)
    {
        _bulletDamage = bulletDamage;
        _shootPoint = gunPoint.forward;
        _sourceObject = sourceObject;
    }
    #endregion
    #region On Trigger
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(_sourceObject.name +" "+other.gameObject.name);
        if ((_sourceObject.gameObject != other.gameObject) && other.gameObject.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.TakeDamage(_bulletDamage);
            gameObject.SetActive(false);
        }
    }
    #endregion
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if ((_sourceObject!=collision.gameObject) && collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
    //    {
    //        damageable.TakeDamage(_bulletDamage);
    //        gameObject.SetActive(false);
    //    }
    //}
}
