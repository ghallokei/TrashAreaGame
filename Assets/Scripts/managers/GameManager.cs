using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    public List<GameObject> garbageList = new List<GameObject>();
    public List<GameObject> garbagePrefabs = new List<GameObject>();

    public GameObject healthPack;

    private int maxEnemies => maxTurrets + maxWalkableEnemies;
    private int maxTurrets = 2;
    private int maxWalkableEnemies = 2;
    private int maxGarbage = 10;
    private float time = 0;

    public float healthTime = 0;
    public GameObject enemyParent;
    public GameObject garbageParent;
    public GameObject player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        healthTime += Time.deltaTime;
        CheckHealth();

        if (healthTime > Random.Range(35, 60))
        {
            SpawnHealth();
            Debug.Log("health spawned");
            healthTime = 0;
        }

        if (time > Random.Range(1, 4))
        {
            SpawnGarbage();
            SpawnEnemies();
            time = 0;
        }
    }

    private void SpawnHealth()
    {
        Instantiate(healthPack,
            new Vector3(Random.Range(-35, 35), 2.3f, Random.Range(-33, 33)),
            Quaternion.Euler(new Vector3(-90, Random.Range(0, 360), 0)));
        return;
    }

    private void SpawnEnemies()
    {
        if (enemyList.Count >= maxEnemies - 1) return;

        enemyList.Add(Instantiate(
            enemyPrefabs[Random.Range(0, enemyPrefabs.Count)],
            new Vector3(Random.Range(-35, 35), 2.5f, Random.Range(-33, 33)), Quaternion.identity));

        enemyList.ForEach(enemy => enemy.transform.parent = enemyParent.transform);
    }

    void SpawnGarbage()
    {
        if (garbageList.Count >= maxGarbage - 1) return;

        garbageList.Add(Instantiate(garbagePrefabs[Random.Range(0, garbagePrefabs.Count)],
            new Vector3(Random.Range(-35, 35), 2.3f, Random.Range(-33, 33)),
            Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))));

        garbageList.ForEach(garbage => garbage.transform.parent = garbageParent.transform);
    }

    public void RemoveEnemy(GameObject currentEnemy)
    {
        enemyList.Remove(currentEnemy);
    }

    private void CheckHealth()
    {
        if (player.GetComponent<Player>().health >= 0) return;

        SceneManager.LoadScene("GameOverScene");
        Destroy(this);
    }
}