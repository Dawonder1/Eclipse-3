using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;

    public bool isGameOver = false;
    float minEnemies = 0, maxEnemies = 1;
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
            spawnNextWave(Mathf.FloorToInt(maxEnemies-minEnemies) );
        }
    }

    void spawnNextWave(int numEnemy)
    {
        for (int i = 0; i < numEnemy; i++)
        {
            //Generate Random Position
            float spawnPosX = randomPos();
            float spawnPosZ = randomPos();
            Vector3 spawnPos = new Vector3(spawnPosX, 0, spawnPosZ);
            Debug.Log(spawnPos);

            //Spawn enemy
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
        minEnemies += 0.5f;
        maxEnemies++;
    }

    float randomPos()
    {
        return Random.Range(0,5) > 2 ? Random.Range(6,25) : -Random.Range(6,25);
    }
}