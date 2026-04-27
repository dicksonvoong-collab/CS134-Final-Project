using System.Collections.Generic;
using TMPro;
using UnityEngine;

//helper class to set up multiple enemy types
[System.Serializable]
public class EnemyType
{
    public string name;
    public GameObject prefab;
    [Range(0, 100)]
    public int spawnWeight;
    public int minWave;
}

public class EnemySpawn : MonoBehaviour
{
    public EnemyType[] enemyTypes;
    public PlayerEXP playerXP;
    public Transform playerLocation;
    public TextMeshProUGUI waveText;

    public float minSpawnDistance = 15f;
    public float maxSpawnDistance = 40f;

    public int currentWave;
    public int enemiesPerWave;
    public float waveTimer;
    public int difficulty = 2;

    private int enemiesRemaining;
    private bool spawningAllowed;

    //first wave starts immediately
    void Start()
    {
        currentWave = 0;
        enemiesPerWave = 6;
        waveTimer = 1f;
        enemiesRemaining = 0;
        spawningAllowed = true;

        spawnWave();
    }

    // spawns next wave when player is alive and enemies are out
    void Update()
    {
        if (!spawningAllowed && enemiesRemaining <= 0 && playerLocation != null)
        {
            Invoke("spawnWave", waveTimer);
            spawningAllowed = true;
        }
    }

    //wave spawning logic
    void spawnWave()
    {
        spawningAllowed = false;
        currentWave++;
        waveText.text = "Wave: " + currentWave.ToString();

        int enemiesToSpawn = enemiesPerWave + (currentWave * difficulty);
        enemiesRemaining = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            spawnEnemy();
        }
    }

    //individual enemy spawning logic
    void spawnEnemy()
    {
        EnemyType selectedPrefab = GetRandomEnemyPrefab();
        if (selectedPrefab == null)
        {
            return;
        }

        RaycastHit checkSpawn;
        
        Vector2 spawnDirection = Random.insideUnitCircle.normalized;
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector3 spawnPosition = playerLocation.position + new Vector3(spawnDirection.x, 0f, spawnDirection.y) * distance;

        if (Physics.Raycast(spawnPosition + Vector3.up * 10f, Vector3.down, out checkSpawn, 20f))
        {
            GameObject enemy = Instantiate(selectedPrefab.prefab, checkSpawn.point + Vector3.up, Quaternion.identity);

            if (enemy != null)
            {
                if (selectedPrefab.name == "Cactus")
                {
                    EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
                    EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                    enemyStats.spawner = this;
                    enemyMovement.playerLocation = playerLocation;

                    enemyStats.health = 1 + Mathf.RoundToInt(2 * currentWave/5);
                    enemyStats.damage = 1 + Mathf.RoundToInt(2 * currentWave / 5);
                    enemyStats.expValue = Mathf.RoundToInt(5 * currentWave / 3);
                    enemyStats.playerXP = playerXP;
                }

                if (selectedPrefab.name == "Mushroom")
                {
                    EnemyJumpMovement enemyMovement = enemy.GetComponent<EnemyJumpMovement>();
                    EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();

                    enemyStats.spawner = this;
                    enemyMovement.playerLocation = playerLocation;

                    enemyStats.health = 10 + Mathf.CeilToInt(3 * currentWave / 5);
                    enemyStats.damage = 2 + Mathf.CeilToInt(2 * currentWave / 5);
                    enemyStats.expValue = Mathf.RoundToInt(15 * currentWave / 3);
                    enemyStats.playerXP = playerXP;
                }
            }
        }
    }

    //randomizing enemy logic
    EnemyType GetRandomEnemyPrefab()
    {
        List<EnemyType> spawnable = new List<EnemyType>();
        int totalWeight = 0;

        foreach (var enemy in enemyTypes)
        {
            if (currentWave >= enemy.minWave)
            {
                spawnable.Add(enemy);
                totalWeight += enemy.spawnWeight;
            }
        }

        if (spawnable.Count == 0)
        {
            return null;
        }

        int randomValue = Random.Range(0, totalWeight);
        int weight = 0;

        foreach (var enemy in spawnable)
        {
            weight += enemy.spawnWeight;
            if (randomValue <= weight)
            {
                return enemy;
            }
        }

        return null;

    }

    //tick down enemies remaining on enemy death
    public void OnEnemyDeath()
    {
        enemiesRemaining--;
    }
}
