using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.AI;

public enum PowerUpType
{
    scoreDoubler,
    bomb,
    slowDown,
    health,
    potion,
    shield,
}
public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerType;
    [SerializeField] float range;
    [SerializeField] float duration;
    [SerializeField] int health;
    [SerializeField] ParticleSystem bombfx;
    bool isActive = false;
    PlayerController playerController;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        Invoke("deSpawn", 10f);
    }
    private void OnMouseDown()
    {
        isActive = true;

        if(powerType == PowerUpType.bomb)
        {
            destroyNearbyEnemies();
        }

        else if(powerType == PowerUpType.scoreDoubler)
        {
            StartCoroutine(doubleScores());
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }

        else if (powerType == PowerUpType.slowDown)
        {
            StartCoroutine(slowDownEnemies());
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }

        else if(powerType == PowerUpType.health)
        {
            increaseHealth();
        }

        else if(powerType == PowerUpType.shield)
        {
            StartCoroutine(shieldTower());
        }
        
        else if(powerType == PowerUpType.potion)
        {
            recover();
        }
    }

    IEnumerator doubleScores()
    {
        GameManager.singleton.doubleScore = true;
        yield return new WaitForSeconds(duration);
        GameManager.singleton.doubleScore = false;
        isActive = false;
        Destroy(gameObject);
    }

    IEnumerator slowDownEnemies()
    {
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.GetComponent<NavMeshAgent>().speed = 1;
        }

        yield return new WaitForSeconds(duration);
        
        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.GetComponent<NavMeshAgent>().speed = 2;
        }
        Debug.Log("Reverting Speed");
        isActive = false;
        Destroy(gameObject);
    }

    void destroyNearbyEnemies()
    {
        for (int i = 0; i < FindObjectsOfType<EnemyController>().Length; i++)
        {
            EnemyController enemy = FindObjectsOfType<EnemyController>()[i];
            if (Vector3.Distance(enemy.transform.position, transform.position) <= range) Destroy(enemy.gameObject);
            Debug.Log(FindObjectsOfType<EnemyController>().Length - i);
        }
        Instantiate(bombfx, transform.position, bombfx.transform.rotation);
        isActive = false;
        Destroy(gameObject);
    }

    void increaseHealth()
    {
        FindObjectOfType<PlayerController>().lives += health;
        FindObjectOfType<PlayerController>().healthfx.Play();
        FindObjectOfType<UIManager>().addLive(FindObjectOfType<PlayerController>().lives);
        isActive = false;
        Destroy(gameObject);
    }

    IEnumerator shieldTower()
    {
        playerController.shieldfx.Play();
        FindObjectOfType<PlayerController>().isShielded = true;
        transform.position = new Vector3(0, -3f, 0);
        yield return new WaitForSeconds(duration);
        playerController.isShielded = false;
        playerController.shieldfx.Stop();
        isActive = false;
        Destroy(gameObject);
    }

    void recover()
    {
        FindObjectOfType<PlayerController>().healthfx.Play();
        for (int i = FindObjectOfType<PlayerController>().lives; i <= 5; i++)
        {
            FindObjectOfType<PlayerController>().lives++;
            FindObjectOfType<UIManager>().addLive(FindObjectOfType<PlayerController>().lives);
        }
        Mathf.Clamp(FindObjectOfType<PlayerController>().lives, 0, 5);
        isActive = false;
        Destroy(gameObject);
    }

    void deSpawn()
    {
        //Despwans if powerup is inactive
        if(!isActive) Destroy(gameObject);
    }
}