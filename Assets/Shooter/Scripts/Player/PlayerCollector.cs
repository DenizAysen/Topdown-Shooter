using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CommonVariables;

[RequireComponent(typeof(PlayerHealth),typeof(PlayerShooter))]
public class PlayerCollector : MonoBehaviour
{
    #region Fields
    private PlayerHealth _playerHealth;
    private PlayerShooter _playerShooter;
    #endregion
    #region Unity Methods
    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerShooter = GetComponent<PlayerShooter>();
    }
    #endregion
    #region OnTrigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<ICollectable>(out var collectItem))
        {
            switch (collectItem.ItemType)
            {
                case CollectableType.GunUpgrade:
                    GunUpgrade(collectItem);
                    break;

                case CollectableType.Health:
                    CollectHealth(collectItem);
                    break;

                default:
                    break;
            }
        }
    }
    #endregion
    #region Private Methods
    void GunUpgrade(ICollectable collectItem)
    {
        bool collectionSuccess = _playerShooter.TakeGunUpgrade();
        if (collectionSuccess)
        {
            collectItem.Collected();
        }
    }
    void CollectHealth(ICollectable collectHealth)
    {
        bool collectionSuccess = _playerHealth.TakeHealth(20);
        if (collectionSuccess)
        {
            collectHealth.Collected();
        }
    }
    #endregion
}
