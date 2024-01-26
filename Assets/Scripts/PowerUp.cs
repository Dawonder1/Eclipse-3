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
    public PowerUpType powerType;
    public int numShields = 0;
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
            fillHealth();
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
        isActive = true;
        GameManager.singleton.doubleScore = true;
        yield return new WaitForSeconds(duration);
        GameManager.singleton.doubleScore = false;
        isActive = false;
        Destroy(gameObject);
    }

    IEnumerator slowDownEnemies()
    {
        isActive = true;
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach(EnemyController enemy in enemies)
        {
            enemy.gameObject.GetComponent<NavMeshAgent>().speed = 0.5f;
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
        Destroy(gameObject);
    }

    void fillHealth()
    {
        FindObjectOfType<PlayerController>().addLive();
        FindObjectOfType<PlayerController>().healthfx.Play();
        FindObjectOfType<UIManager>().addLive(FindObjectOfType<PlayerController>().lives);
        Destroy(gameObject);
    }

    IEnumerator shieldTower()
    {
        if (FindObjectOfType<PlayerController>().numShields > 0)
        {
            playerController.numShields++;
            Destroy(gameObject);
        }
        else
        {
            isActive = true;
            FindObjectOfType<PlayerController>().shieldfx.Play();
            FindObjectOfType<PlayerController>().numShields++;
            transform.position = new Vector3(0, -3f, 0);
            while (FindObjectOfType<PlayerController>().numShields > 0)
            {
                yield return new WaitForSeconds(duration);
                FindObjectOfType<PlayerController>().numShields--;
            }
            FindObjectOfType<PlayerController>().shieldfx.Stop();
            isActive = false;
            Destroy(gameObject);
        }
    }

    void recover()
    {
        FindObjectOfType<PlayerController>().healthfx.Play();
        for (int i = FindObjectOfType<PlayerController>().lives; i <= 5; i++)
        {
            FindObjectOfType<PlayerController>().addLive();
            FindObjectOfType<UIManager>().addLive(FindObjectOfType<PlayerController>().lives);
        }
        Destroy(gameObject);
    }

    void deSpawn()
    {
        //Despwans if powerup is inactive
        if(!isActive) Destroy(gameObject);
    }
}