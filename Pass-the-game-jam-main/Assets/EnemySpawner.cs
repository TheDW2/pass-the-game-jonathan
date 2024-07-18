using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Spawn enemies at regular intervals scaling with difficulty
public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;

    [SerializeField]
    float spawnRate = 100;
    float lastSpawn = -90;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastSpawn >= spawnRate)
        {
            for (int i = (int)GameManger.instance.difficulty; i > 0; i--)
            {
                Invoke("SpawnEnemy", i);
            }
            lastSpawn = Time.time; 
            GameManger.instance.QueueNotif("Mobs Coming");
            GameManger.instance.difficulty *= 1.2f;
            spawnRate = Mathf.Clamp(spawnRate - Mathf.Pow(2, GameManger.instance.difficulty), 60, Mathf.Infinity);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
