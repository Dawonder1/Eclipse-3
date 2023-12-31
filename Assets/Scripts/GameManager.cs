using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool isGameOver = false;
    int maxEnemies = 1;
    int minEnemies = 0;
    public int score = 0;
    [SerializeField] GameObject enemyPrefab;
    public float musicVolume, soundVolume;
    public bool doubleScore = false;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this);
        }
        soundVolume = PlayerPrefs.GetFloat("soundVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);
        GetComponent<AudioSource>().volume = musicVolume;
    }

    void Update()
    {
        int enemies = FindObjectsOfType<EnemyController>().Length;
        if (!isGameOver && enemies <= minEnemies)
        {
            spawnNextWave(maxEnemies++ - minEnemies);
        }
    }

    void spawnNextWave(int numEnemy)
    {
        for (int i = 0; i < numEnemy; i++)
        {
            //Generate Random Position
            float spawnPosX = Random.Range(-range(), range());
            float spawnPosZ = Random.Range(-range(), range());
            Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);

            //Spawn enemy
            Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation);
        }
    }

    float range()
    {
        return Random.Range(10, 25);
    }
}