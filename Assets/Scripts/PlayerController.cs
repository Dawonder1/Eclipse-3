using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int lives = 3;
    Rigidbody rb;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] ParticleSystem damagefx, healthfx;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public IEnumerator takeDamage()
    {
        lives--;
        damagefx.Play();
        if (lives <= 0) gameOver();
        yield return new WaitForSeconds(1f);
        damagefx.Stop();
        
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