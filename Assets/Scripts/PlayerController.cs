using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int lives = 3;
    Rigidbody rb;
    [SerializeField] GameObject gameOverPanel;
    public ParticleSystem damagefx, healthfx;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void takeDamage()
    {
        lives--;
        damagefx.Play();
        if (lives <= 0) gameOver();        
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