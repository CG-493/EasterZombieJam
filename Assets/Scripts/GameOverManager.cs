using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text finalScoreText;
    public Text highScoreText;

    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        int score = ScoreManager.instance.score;
        int highScore = ScoreManager.instance.highScore;

        finalScoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;

        Time.timeScale = 0f;
    }
}