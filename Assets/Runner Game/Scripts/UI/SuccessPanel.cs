using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessPanel : PanelBase
{
    #region Unity Fields
    [SerializeField] TextMeshProUGUI finalScoreText;
    [SerializeField] TextMeshProUGUI currentScoreText;
    #endregion
    #region Unity Methods
    private void Start()
    {
        finalScoreText.text = 0.ToString();
    } 
    #endregion
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
    #region Private Methods
    private void SetFinalScore()
    {
        finalScoreText.text = currentScoreText.text;
    } 
    #endregion
}
