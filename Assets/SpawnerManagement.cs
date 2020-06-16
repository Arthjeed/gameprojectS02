using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManagement : MonoBehaviour
{
    public int enemyMinLevel;
    public int enemyMaxLevel;
    public float radius;
    public float frequence;
    public int enemyNumbers;

    public GameObject EnemyShipWeak;
    public GameObject EnemyShipMedium;

    private int weakMinLvl = 0;
    private int mediumMinLvl = 5;
    private int hardMinLvl = 10;
    private float timer = 0;
    private float distancePlayer = 300;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship");
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            if (Vector3.Distance(transform.position, player.transform.position) > distancePlayer)
                SpawnEnemies();
            timer = frequence;
        }
    }

    private void SpawnEnemies()
    {
        int level;
        print("spawn");
        for (int i = 0; i < enemyNumbers; i++)
        {
            level = Random.Range(enemyMinLevel, enemyMaxLevel + 1);
            if (level > weakMinLvl && level < mediumMinLvl)
                Spawn(EnemyShipWeak, level);
            else if (level >= mediumMinLvl && level < hardMinLvl)
                Spawn(EnemyShipMedium, level);
        }
    }

    private void Spawn(GameObject unit, int level)
    {
        GameObject enemy = Instantiate(unit, transform);
        enemy.GetComponent<EnemyShipBehavior>().ShipPower = level;
        enemy.transform.position = enemy.transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius), 0);
    }
}
