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
}
public class PowerUp : MonoBehaviour
{
    [SerializeField] PowerUpType powerType;
    [SerializeField] float range;
    [SerializeField] float duration;
    private void OnMouseDown()
    {
        if(powerType == PowerUpType.bomb)
        {
            for(int i = 0; i < FindObjectsOfType<EnemyController>().Length; i++)
            {
                EnemyController enemy = FindObjectsOfType<EnemyController>()[i];
                if( Vector3.Distance(enemy.transform.position, transform.position) <= range) Destroy(enemy.gameObject);
                Debug.Log(FindObjectsOfType<EnemyController>().Length - i);
            }
            Destroy(gameObject);
        }

        if(powerType == PowerUpType.scoreDoubler)
        {
            StartCoroutine(doubleScores());
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }

        if (powerType == PowerUpType.slowDown)
        {
            StartCoroutine(slowDownEnemies());
            transform.position = new Vector3(transform.position.x, -10f, transform.position.z);
        }
    }

    IEnumerator doubleScores()
    {
        GameManager.singleton.doubleScore = true;
        yield return new WaitForSeconds(duration);
        GameManager.singleton.doubleScore = false;
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
        Destroy(gameObject);
    }
}