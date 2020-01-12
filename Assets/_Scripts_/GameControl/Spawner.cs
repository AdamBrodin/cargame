#pragma warning disable CS0649
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Color gizmoColor = Color.red;

    public enum SpawnTypes
    {
        Normal,
        Wave,
        TimedWave
    }

    public enum EnemyLevels
    {
        Easy,
        Medium,
        Hard,
    }

    public EnemyLevels enemyLevel = EnemyLevels.Easy;

    public GameObject[] EasyEnemy;
    public GameObject[] MediumEnemy;
    public GameObject[] HardEnemy;

    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);

    public int totalEnemy = 10;
    private int numEnemy = 0;
    private int spawnedEnemy = 0;
    //----------------------------------
    // End of Enemy Settings
    //----------------------------------

    private int SpawnID;
    public Transform[] spawnPoints;

    private bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.Normal;


    // timed wave controls

    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;
    [SerializeField] private float lastSpawned;
    [SerializeField] private float spawnRate;

    //Wave controls

    public int totalWaves = 5;
    private int numWaves = 0;


    void Start()
    {
        SpawnID = Random.Range(1, 500);
        Enemies.Add(EnemyLevels.Easy, EasyEnemy[0]);
        Enemies.Add(EnemyLevels.Medium, MediumEnemy[0]);
        Enemies.Add(EnemyLevels.Hard, HardEnemy[0]);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }

    void Update()
    {
        if (Spawn)
        {
            if (spawnType == SpawnTypes.Normal)
            {
                if (numEnemy < totalEnemy)
                {
                    spawnEnemy();
                }
            }

            else if (spawnType == SpawnTypes.Wave)
            {
                if (numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {

                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        waveSpawn = true;
                        numWaves++;
                    }
                    if (numEnemy == totalEnemy)
                    {
                        waveSpawn = false;
                    }
                }
            }

            else if (spawnType == SpawnTypes.TimedWave)
            {
                if (numWaves <= totalWaves)
                {
                    timeTillWave += Time.deltaTime;
                    if (waveSpawn)
                    {
                        spawnEnemy();
                    }

                    if (timeTillWave >= waveTimer)
                    {
                        waveSpawn = true;
                        timeTillWave = 0.0f;
                        numWaves++;
                        numEnemy = 0;
                    }

                    if (numEnemy >= totalEnemy)
                    {
                        waveSpawn = false;
                    }
                }

                else
                {
                    Spawn = false;
                }
            }
        }
    }

    private void spawnEnemy()
    {
        if (Time.time - lastSpawned > 1 / spawnRate)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            int enemyLevelIndex = Random.Range(0, EasyEnemy.Length);
            Instantiate(EasyEnemy[enemyLevelIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
            spawnPoints[spawnPointIndex].rotation = Quaternion.AngleAxis(Random.Range(0, 360), transform.forward) * transform.rotation;
            numEnemy++;
            spawnedEnemy++;
        }
    }

    public void enableSpawner(int sID)
    {
        if (SpawnID == sID)

        {
            Spawn = true;
        }
    }

    public void disableSpawner(int sID)
    {
        if (SpawnID == sID)
        {
            Spawn = false;
        }
    }

    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }

    public void enableTrigger()
    {
        Spawn = true;
    }
}


