using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EnemyController : MonoBehaviour
{
    float soundVolume;
    Transform player;
    NavMeshAgent agent;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] GameObject[] powerUp;
    //[SerializeField] AudioClip clip;

    void Start()
    {
        soundVolume = GameManager.singleton.soundVolume;
        //audioSource = FGetComponent<AudioSource>();
        //audioSource.volume = soundVolume;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
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
        return Vector3.Distance(transform.position, player.position) < 5.2f;
    }

    private void attack()
    {
        //audioSource.PlayOneShot(clip);
        transform.LookAt(player.position);
        animator.SetFloat("speed", 0);
        animator.SetTrigger("attack");
        //Debug.Log(audioSource.isPlaying);

        Destroy(gameObject, 2);
    }

    private void OnDestroy()
    {
        //spawn random powerup
        if(Random.Range(0, 1f) <= 0.1f)
        {
            int prefabIndex = Random.Range(0, powerUp.Length);
            Instantiate(powerUp[prefabIndex], transform.position, powerUp[prefabIndex].transform.rotation);
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.singleton.isGameOver) return;
        //update score
        GameManager.singleton.score++;
        if (GameManager.singleton.doubleScore) GameManager.singleton.score++;
        FindObjectOfType<UIManager>().updateScore(GameManager.singleton.score);

        //kill enemy
        Destroy(gameObject);        
    }

    void dealDamage()
    {
        player.GetComponent<PlayerController>().takeDamage();
    }
}