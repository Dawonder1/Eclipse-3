using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;
    //[SerializeField] GameObject potion;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("target").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
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
            animator.SetBool("isAttacking", false);
            animator.SetFloat("speed", agent.velocity.magnitude);
            agent.destination = target.position;
        }
    }

    bool isInRange()
    {
        if(Vector3.Distance(transform.position, target.position) < 5.2f)
        {
            return true;
        }
        return false;
    }

    private void attack()
    {
        transform.LookAt(target.position);
        animator.SetFloat("speed", 0);
        animator.SetTrigger("attack");
        Destroy(gameObject, 3f);
        //agent.isStopped = true;;
        Debug.Log("attacking player");
    }

    private void OnDestroy()
    {
        if(Random.Range(0, 1f) <= 0.05)
        {
            //Instantiate(health);
        }
        //GameManager.singleton.score++;
        Debug.Log(GameManager.singleton.score);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
        Debug.Log("You killed this enemy");
        GameManager.singleton.score++;
    }
}