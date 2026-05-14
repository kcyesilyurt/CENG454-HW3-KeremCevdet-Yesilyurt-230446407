using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int maxWaves = 3;
    [SerializeField] private float timeBetweenWaves = 2f;

    [Header("Enemy Settings")]
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;

    [Header("Game State")]
    [SerializeField] private GameStateController gameStateController;

    [Header("Wave Enemy Counts")]
    [SerializeField] private int waveOneEnemyCount = 3;
    [SerializeField] private int waveTwoEnemyCount = 5;
    [SerializeField] private int waveThreeEnemyCount = 7;

    private readonly List<EnemyController> aliveEnemies = new List<EnemyController>();

    private int currentWave;
    private bool isSpawning;
    private bool wavesCompleted;

    public event Action<int, int> OnWaveChanged;
    public event Action OnAllWavesCompleted;

    public int CurrentWave => currentWave;
    public int MaxWaves => maxWaves;

    private void Start()
    {
        StartCoroutine(StartNextWaveAfterDelay(1f));
    }

    private IEnumerator StartNextWaveAfterDelay(float delay)
    {
        if (IsGameOver())
        {
            yield break;
        }

        isSpawning = true;
        yield return new WaitForSeconds(delay);

        if (!IsGameOver())
        {
            StartNextWave();
        }

        isSpawning = false;
    }

    private void StartNextWave()
    {
        if (wavesCompleted || IsGameOver())
        {
            return;
        }

        currentWave++;

        if (currentWave > maxWaves)
        {
            CompleteAllWaves();
            return;
        }

        OnWaveChanged?.Invoke(currentWave, maxWaves);

        int enemyCount = GetEnemyCountForWave(currentWave);

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(i);
        }
    }

    private int GetEnemyCountForWave(int waveNumber)
    {
        switch (waveNumber)
        {
            case 1:
                return waveOneEnemyCount;
            case 2:
                return waveTwoEnemyCount;
            case 3:
                return waveThreeEnemyCount;
            default:
                return waveThreeEnemyCount;
        }
    }

    private void SpawnEnemy(int spawnIndex)
    {
        if (IsGameOver())
        {
            return;
        }

        if (enemyPrefab == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("WaveManager is missing enemy prefab or spawn points.");
            return;
        }

        Transform spawnPoint = spawnPoints[spawnIndex % spawnPoints.Length];

        EnemyController enemy = Instantiate(
            enemyPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        enemy.OnEnemyDied += HandleEnemyDied;
        aliveEnemies.Add(enemy);
    }

    private void HandleEnemyDied(EnemyController enemy)
    {
        if (enemy == null)
        {
            return;
        }

        enemy.OnEnemyDied -= HandleEnemyDied;
        aliveEnemies.Remove(enemy);

        if (aliveEnemies.Count == 0 && !isSpawning && !wavesCompleted && !IsGameOver())
        {
            StartCoroutine(StartNextWaveAfterDelay(timeBetweenWaves));
        }
    }

    private void CompleteAllWaves()
    {
        if (wavesCompleted || IsGameOver())
        {
            return;
        }

        wavesCompleted = true;
        OnAllWavesCompleted?.Invoke();
        Debug.Log("All waves completed.");
    }

    private bool IsGameOver()
    {
        return gameStateController != null && gameStateController.IsGameOver;
    }
}