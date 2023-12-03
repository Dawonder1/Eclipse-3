using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int lives = 3;
    Rigidbody rb;
    [SerializeField] GameObject gameOverPanel;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void takeDamage()
    {
        lives--;
        if(lives <= 0)gameOver();
    }
    void gameOver()
    {
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