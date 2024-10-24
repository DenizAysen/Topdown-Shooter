using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonCreator<GameManager>
{
    #region Fields
    PlayerHealth _playerHealth;
    int _currentScore = 0;
    #endregion
    #region Unity Fields
    [Header("Panels")]
    [SerializeField] PanelBase inGamePanel;
    [SerializeField] PanelBase startPanel;
    [SerializeField] PanelBase failedPanel;
    [SerializeField] PanelBase successPanel;
    [SerializeField] TextMeshProUGUI inGameScoreText;
    [SerializeField] TextMeshProUGUI finalGameScoreText;
    [SerializeField] int scorePerKill = 50;
    [Header("Collectable Settings")]
    [SerializeField] float bonusYOffset;
    [SerializeField] List<CollectableBase> collectableItems;
    #endregion
    #region Actions
    public static Action onGameFinished;
    #endregion
    #region Unity Methods
    private void OnEnable()
    {
        EnemyBase.onEnemyKilled += OnEnemyKilled;
        PlayerHealth.onPlayerDied += OnPlayerDied;
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
        PlayerHealth.onPlayerDied -= OnPlayerDied;
        
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
        _currentScore += scorePerKill;
        inGameScoreText.text = _currentScore.ToString();
        bool killedAllEnemies = true;
        var allEnemies = FindObjectsOfType<EnemyBase>();
        if(allEnemies.ToList().Any(x => !x.IsDead))
        {
            killedAllEnemies = false;
        }
        else
        {
            DisableAllPanels();
            finalGameScoreText.text = _currentScore.ToString();
            successPanel.gameObject.SetActive(true);
            killedAllEnemies = true;
            onGameFinished?.Invoke();
        }
        CreateRandomBonus(killedPos);
    }
    private void OnPlayerDied()
    {
        DisableAllPanels();
        failedPanel.gameObject.SetActive(true);
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
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        buildIndex++;
        int sceneCount = SceneManager.sceneCount;
        if (buildIndex >= sceneCount)
        {
            buildIndex = 0;
        }
        SceneManager.LoadScene(buildIndex);
    }
    #endregion
}
