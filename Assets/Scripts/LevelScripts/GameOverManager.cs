using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    public int currentScore;
    public int highScore;

    public GameObject sManager;

    private void Awake()
    {
        sManager = GameObject.Find("ScoreManagerObject");
    }

    public void GameOver()
    {
        ScoreManager manager = sManager.GetComponent<ScoreManager>();
        gameOverPanel.SetActive(true);

        currentScore = manager.score;
        highScore = manager.highScore;


        if(finalScoreText != null)
           finalScoreText.text = "Score: " + currentScore;
        
        
        if(highScoreText != null)
          highScoreText.text = "High Score: " + highScore;

        Time.timeScale = 0f;
    }
}