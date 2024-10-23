using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : SingletonCreator<GameManager>
{
    #region Fields
    PlayerHealth _playerHealth;
    #endregion
    #region Unity Fields
    [Header("Panels")]
    [SerializeField] PanelBase inGamePanel;
    [SerializeField] PanelBase startPanel;
    [SerializeField] PanelBase failedPanel;
    [SerializeField] PanelBase successPanel;
    [SerializeField] TextMeshProUGUI inGameScoreText;
    [SerializeField] int scorePerKill = 50;
    [Header("Collectable Settings")]
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
        DisableAllPanels();
        startPanel.gameObject.SetActive(true);
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
    private void DisableAllPanels()
    {
        inGamePanel.gameObject.SetActive(false);
        startPanel.gameObject.SetActive(false);
        failedPanel.gameObject.SetActive(false);
        successPanel.gameObject.SetActive(false);
    }
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
    #region Public Methods
    public void StartGame()
    {
        DisableAllPanels();
        inGamePanel.gameObject.SetActive(true);
    }
    #endregion
}
