using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPointPrefab;
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    private float timeSpawn = 0.5f;

    private int size;
    private GameObject[,] spawnPoints;
    private int canSpawnCount;

    void Start()
    {
        size = gameSettings.size;
        canSpawnCount = size * size;
        spawnPoints = new GameObject[size, size];

        int startPoint = -size + 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                Vector3 newPos = new Vector3(startPoint + i * 2, 0, startPoint + j * 2);
                spawnPoints[i, j] = Instantiate(spawnPointPrefab, newPos, Quaternion.identity, this.transform);
            }
        }
        StartCoroutine(FindSpawnPointCoroutine());
    }

    IEnumerator FindSpawnPointCoroutine()
    {
        yield return new WaitForSeconds(timeSpawn);

        if (canSpawnCount != 0)
        {
            while (true)
            {
                int i = UnityEngine.Random.Range(0, size);
                int j = UnityEngine.Random.Range(0, size);
                SpawnPoint spawnPoint = spawnPoints[i, j].GetComponent<SpawnPoint>();
                if (!spawnPoint.isMole)
                {
                    spawnPoint.Spawn();
                    canSpawnCount--;
                    break;
                }
            }
        }
        StartCoroutine(FindSpawnPointCoroutine());
    }


    public void AddCanSpawnCount()
    {
        canSpawnCount++;
    }
}
