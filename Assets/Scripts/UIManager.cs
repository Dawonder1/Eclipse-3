using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] lives;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void updateScore(int score)
    {
        scoreText.text = "Score: " + score; 
    }
    public void removeLive(int live)
    {
        lives[live-1].SetActive(false);
    }

    public void addLive(int livesNow)
    {
        lives[livesNow-1].SetActive(true);
    }
    public void gameOver()
    {
        GameManager.singleton.isGameOver = true;
        int highscore = PlayerPrefs.GetInt("HighScore", 0);
        if (GameManager.singleton.score > highscore)
        {
            PlayerPrefs.SetInt("HighScore", GameManager.singleton.score);
            highScoreText.text = "New HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);
        }
        gameOverPanel.SetActive(true);
    }
    public void playAgain()
    {
        SceneManager.LoadScene("Gym");
    }
    public void goToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
