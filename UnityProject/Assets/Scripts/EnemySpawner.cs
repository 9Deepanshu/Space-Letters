using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using System.Security.Cryptography;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject enemy2;

    public float spawnRate = 2.0f;
    private float spawnTimer;
    private string[] enemyLetters = {"q", "e", "r", "t", "g", "f", "v", "c", "x", "z"};

    private float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        startDelay = Time.time + 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startDelay)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (Time.time > spawnTimer)
        {
            Instantiate(enemy, RandomEnemyPosition(), transform.rotation);

            GameObject letterEnemy = Instantiate(enemy2, RandomEnemyPosition(), transform.rotation);
            int randomNum = Random.Range(0,enemyLetters.Length);
            letterEnemy.tag = enemyLetters[randomNum];
            GameManager.instance.spawningEnemy = enemyLetters[randomNum].ToUpper();

            spawnTimer = Time.time + spawnRate;
        }
    }

    private Vector3 RandomEnemyPosition()
    {   
        if (Random.Range(0,2) == 0)
        {
            float randomX = Random.Range(-150.0f,150.0f);
            float randomZ = Random.Range(0,2) == 0 ? -100.0f : 100.0f;
            return new Vector3(randomX, 0, randomZ);
        }
        else
        {
            float randomZ = Random.Range(-100.0f,100.0f);
            float randomX = Random.Range(0,2) == 0 ? -150.0f : 150.0f;
            return new Vector3(randomX, 0, randomZ);
        }
    }
}
