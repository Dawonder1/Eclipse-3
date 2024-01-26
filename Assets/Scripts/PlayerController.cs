using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int maxLives, lives = 5;
    public int numShields = 0;
    Rigidbody rb;
    public bool isShielded;
    public ParticleSystem damagefx, healthfx, shieldfx;
    // Start is called before the first frame update
    void Start()
    {
        isShielded = false;
        rb = GetComponent<Rigidbody>();
    }

    public void takeDamage()
    {
        if (numShields > 0) return;
        FindObjectOfType<UIManager>().removeLive(lives--);
        damagefx.Play();
        if (lives <= 0) FindObjectOfType<UIManager>().gameOver();    
    }

    public void addLive()
    {
        if (lives < maxLives)
        {
            lives++;
        }
    }
}