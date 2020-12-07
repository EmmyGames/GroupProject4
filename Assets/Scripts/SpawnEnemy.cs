using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemyList;
    public bool isAlive;
    void Awake()
    {
        GameObject enemy = Instantiate(enemyList[Random.Range(0, enemyList.Length)], transform.position,
            Quaternion.identity);
    }

    private void Update()
    {
        
    }

    IEnumerator EnemyKilled()
    {
        yield return new WaitForSeconds(100);
        GameObject enemy = Instantiate(enemyList[Random.Range(0, enemyList.Length)], transform.position,
            Quaternion.identity);
    }
}
