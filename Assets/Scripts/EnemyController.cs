using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    int health = GameManager.singleton.health;
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    //[SerializeField] GameObject potion;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange())
        {
            attack();
        }
        else
        {
            agent.destination = target.position;
        }
    }

    bool isInRange()
    {
        if(Vector3.Distance(agent.destination, target.position) < 2)
        {
            return true;
        }
        return false;
    }

    private void attack()
    {
        animator.SetTrigger("attack");
        Destroy(this.gameObject, 1f);
        Debug.Log("attacking player");
    }

    private void OnDestroy()
    {
        if(Random.Range(0, 1f) <= 0.05)
        {
            //Instantiate(health);
        }
    }

    private void OnMouseDown()
    {
        if(health > 0) health--;
        else Destroy(this.gameObject);
        GameManager.singleton.health++;
        GameManager.singleton.score++;
    }
}