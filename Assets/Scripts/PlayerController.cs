using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int lives = 5;
    Rigidbody rb;
    public bool isShielded;
    [SerializeField] GameObject gameOverPanel;
    public ParticleSystem damagefx, healthfx;
    // Start is called before the first frame update
    void Start()
    {
        isShielded = false;
        rb = GetComponent<Rigidbody>();
    }

    public void takeDamage()
    {
        if (isShielded) return;
        FindObjectOfType<UIManager>().removeLive(lives--);
        damagefx.Play();
        if (lives <= 0) gameOver();        
    }
    
    void gameOver()
    {
        GameManager.singleton.isGameOver = true;
    }
}