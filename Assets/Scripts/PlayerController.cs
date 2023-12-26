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
    public ParticleSystem damagefx, healthfx, shieldfx;
    // Start is called before the first frame update
    void Start()
    {
        isShielded = false;
        rb = GetComponent<Rigidbody>();
    }

    public void takeDamage()
    {
        if (isShielded) return;
        Mathf.Clamp(lives, 0, 5);
        FindObjectOfType<UIManager>().removeLive(lives--);
        damagefx.Play();
        if (lives <= 0) FindObjectOfType<UIManager>().gameOver();        
    }
}