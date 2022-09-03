using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    [SerializeField]
    private GameObject boarPrefab, canniblePrefab;
    public Transform cannibleSpawnPoint, boarSpawnPoint;
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
}
