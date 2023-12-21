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
    void gameOver()
    {
        //isGameOver = true;
        //
        gameOverPanel.SetActive(true);
    }
    void playAgain()
    {
        SceneManager.LoadScene("Gym");
    }
    void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
