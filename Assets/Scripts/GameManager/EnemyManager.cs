using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField]
    private GameObject boarPrefab, canniblePrefab;
    public Transform[] cannibleSpawnPoint, boarSpawnPoint;
    [SerializeField]
    private int cannibleEnemyCount, boarEnemyCount;
    private int initialCannibleCount, initialBoarCount;
    public float waitBeforsSpawnEnemiesTime = 10f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        initialCannibleCount = cannibleEnemyCount;
        initialBoarCount = boarEnemyCount;
        SpawnEnemies();
        StartCoroutine("CheckToSpawnEnemies");
    }

    void SpawnEnemies()
    {
        SpawnCannibles();
        SpawnBoars();
    }
    void SpawnCannibles()
    {
        int index = 0;
        for (int i = 0; i < cannibleEnemyCount; i++)
        {
            if (index >= cannibleSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(canniblePrefab, cannibleSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        cannibleEnemyCount = 0;
    }
    void SpawnBoars()
    {
        int index = 0;
        for (int i = 0; i < boarEnemyCount; i++)
        {
            if (index >= boarSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(boarPrefab, boarSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        boarEnemyCount = 0;
    }

    public void EnemyDied(bool cannible)
    {
        if (cannible)
        {
            cannibleEnemyCount++;
            if (cannibleEnemyCount > initialCannibleCount)
            {
                cannibleEnemyCount = initialCannibleCount;
            }
            else
            {
                boarEnemyCount++;
                boarEnemyCount = initialBoarCount;
            }
        }
    }
    IEnumerator CheckToSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforsSpawnEnemiesTime);
        SpawnCannibles();
        SpawnBoars();
        StartCoroutine("CheckToSpawnEnemies");
    }
    public void StopSpawningEnemies()
    {
        StopCoroutine("CheckToSpawnEnemies");
    }
}
