using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    public List<EnemyList> enemies = new List<EnemyList>();
    public int currWave;
    [SerializeField] private int waveValue;
    public List<GameObject> enemiesToSpawn = new List<GameObject>();

    public Transform[] spawnLoc;
    public int spawnIndex;

    public int waveDur;
    private float waveTimer;
    private float spawnInter;
    private float spawnTimer;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    void Start()
    {
        GenerateWave();
    }

    
    void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if(enemiesToSpawn.Count > 0)
            {
                GameObject enemy = (GameObject)Instantiate(enemiesToSpawn[0], spawnLoc[spawnIndex].position,Quaternion.identity);
                enemiesToSpawn.RemoveAt(0);

                spawnedEnemies.Add(enemy);
                spawnTimer = spawnInter;

                if(spawnIndex + 1 <= spawnLoc.Length-1)
                {
                    spawnIndex++;
                }

                else
                {
                    spawnIndex = 0;
                }
            }
        }

        else
        {
            spawnTimer -= Time.deltaTime;
            waveTimer -= Time.deltaTime;
        }

        if(waveTimer <= 0 && spawnedEnemies.Count <= 0)
        {
            currWave++;
            GenerateWave();

        }

    }

    void GenerateWave()
    {
        waveValue = currWave * 10;

        GenerateEnemies();

        spawnInter = waveDur / enemiesToSpawn.Count;
        waveTimer = waveDur;
    }

    void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while(waveValue > 0 || generatedEnemies.Count < 50)
        {
            int randEnemyID = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyID].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                generatedEnemies.Add(enemies[randEnemyID].enemyPrefab);
                waveValue -= randEnemyCost;
            }

            else if (waveValue <= 0)
            {
                break;
            }

        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

}

[System.Serializable]

public class EnemyList
{
    public GameObject enemyPrefab;
    public int cost;
}