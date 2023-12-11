using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    float soundVolume;
    Transform player;
    NavMeshAgent agent;
    Animator animator;
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] GameObject[] powerUp;
    //[SerializeField] GameObject potion;

    void Start()
    {
        soundVolume = GameManager.singleton.soundVolume;
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = soundVolume;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

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
            agent.destination = player.position;
        }
    }

    bool isInRange()
    {
        if(Vector3.Distance(transform.position, player.position) < 5.2f)
        {
            return true;
        }
        return false;
    }

    private void attack()
    {
        transform.LookAt(player.position);
        audioSource.Play();
        animator.SetFloat("speed", 0);
        animator.SetTrigger("attack");

        Destroy(gameObject, 2);
        
    }

    private void OnDestroy()
    {
        if(Random.Range(0, 1f) <= 0.05)
        {
            Instantiate(powerUp[Random.Range(0,1)] );
        }

        Debug.Log(GameManager.singleton.score);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);

        //update score
        GameManager.singleton.score++;
        if(GameManager.singleton.doubleScore) GameManager.singleton.score++;
    }

    void dealDamage()
    {
        player.GetComponent<PlayerController>().takeDamage();
    }
}