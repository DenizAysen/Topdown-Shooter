using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonCreator<GameManager>
{
    #region Fields
    PlayerHealth _playerHealth;
    #endregion
    #region Unity Fields
    [SerializeField] float bonusYOffset;
    [SerializeField] List<CollectableBase> collectableItems;
    #endregion
    #region Unity Methods
    private void OnEnable()
    {
        EnemyBase.onEnemyKilled += OnEnemyKilled;
    }
    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        if (_playerHealth == null)
            Debug.LogError("No player in game");
    }
    private void OnDisable()
    {
        EnemyBase.onEnemyKilled -= OnEnemyKilled;
    }
    #endregion
    #region Private Methods
    private void OnEnemyKilled(Vector3 killedPos)
    {
        CreateRandomBonus(killedPos);
    }
    private void CreateRandomBonus(Vector3 bonusPos)
    {
        int bonusIndex = 0;
        if(_playerHealth.CurrentHealth < 50)
        {
            bonusIndex = 1;
        }
        else
        {
            bonusIndex = 0;
        }
        CollectableBase itemToCreate = collectableItems[bonusIndex];
        var bonusObject = Instantiate(itemToCreate, null);
        bonusPos.y += bonusYOffset;
        bonusObject.transform.position = bonusPos;
    }
    #endregion
}
