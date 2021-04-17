﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private float _waitTime = 5.0f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private int[] _powerUpSpawnProb;
    [SerializeField]
    private GameObject[] _enemies;
    private int[] _enemySpawnProb = new int[3];

    [SerializeField]
    private int _waveCount = 0;
    private List<GameObject> _enemyList = new List<GameObject>();
    private bool _stopSpawning = false;
    UIManager _uIManager;

    public void StartSpawning()
    {
        StartCoroutine(StartWaveSpawnerRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator StartWaveSpawnerRoutine()
    {
        yield return new WaitForSeconds(3.5f);
        while (_stopSpawning == false)
        {
            yield return SpawnWaveRoutine();
            yield return new WaitWhile(EnemyisAlive);
            yield return new WaitForSeconds(_waitTime);
        }
    }

    private bool EnemyisAlive()
    {
        _enemyList = _enemyList.Where(e => e != null).ToList();
        return _enemyList.Count > 0;
    }

    IEnumerator SpawnWaveRoutine()
    {
        _waveCount++;
        AssignProbability();
        for (int i = 0; i < _waveCount; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_waitTime);
        }

    }

    private void AssignProbability()
    {
        switch (_waveCount)
        {
            case 1:
                _enemySpawnProb[0] = 80;
                _enemySpawnProb[1] = 20;
                _enemySpawnProb[2] = 0;
                break;
            case 3:
                _enemySpawnProb[0] = 70;
                _enemySpawnProb[1] = 25;
                _enemySpawnProb[2] = 5;
                break;
            case 5:
                _enemySpawnProb[0] = 60;
                _enemySpawnProb[1] = 30;
                _enemySpawnProb[2] = 10;
                break;
            case 9:
                _enemySpawnProb[0] = 50;
                _enemySpawnProb[1] = 30;
                _enemySpawnProb[2] = 20;
                break;
            default:
                break;
        }

    }

    private void SpawnEnemy()
    {
        int randomEnemy = Random.Range(1, 101);
        int low;
        int high = 0;
        for (int i = 0; i < _enemies.Length; i++)
        {
            low = high;
            high += _enemySpawnProb[i];
            if (randomEnemy >= low && randomEnemy < high)
            {
                Debug.Log("Low was " + low + " & high was " + high + ". Enemy " + i + " spawned.");
                float randomX = Random.Range(-9.5f, 9.5f);
                GameObject newEnemy = Instantiate(_enemies[i], new Vector3(randomX, 8, 0), Quaternion.identity);
                newEnemy.transform.parent = GameObject.Find("Enemy_Container").transform;
                _enemyList.Add(newEnemy);
            }
        }
    }

    private void SpawnPowerup()
    {
        int randomPowerUp = Random.Range(1, 101);
        Debug.Log("Rolling for Powerup. Rolled a " + randomPowerUp);
        int low;
        int high = 0;
        for (int i = 0; i < _powerups.Length; i++)
        {
            low = high;
            high += _powerUpSpawnProb[i];
            if (randomPowerUp >= low && randomPowerUp < high)
            {
                Debug.Log("Low was " + low + " & high was " + high + ". Powerup " + i + " awarded.");
                float randomX = Random.Range(-9.5f, 9.5f);
                Instantiate(_powerups[i], new Vector3(randomX, 8, 0), Quaternion.identity);
            }
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        float randomT = Random.Range(3f, 7f);
        yield return new WaitForSeconds(randomT);
        while (_stopSpawning == false)
        {
            SpawnPowerup();

            //int randomPowerUp = Random.Range(0, 7);
            //if (randomPowerUp == 5)
            //{
            //    Debug.Log("Powerup is " + randomPowerUp + "! Rolling to confirm black hole cannon");
            //    int randomPowerUp2 = Random.Range(0, 7);
            //    randomPowerUp = randomPowerUp2;
            //    Debug.Log("new powerup is " + randomPowerUp);
            //}
            //else if (randomPowerUp == 7)
            //{
            //    Debug.Log("Powerup is " + randomPowerUp + "! Rolling to confirm slow Negaup");
            //    int randomPowerUp2 = Random.Range(0, 7);
            //    randomPowerUp = randomPowerUp2;
            //    Debug.Log("new powerup is " + randomPowerUp);
            //}
            //float randomX = Random.Range(-9.5f, 9.5f);
            //Instantiate(_powerups[randomPowerUp], new Vector3(randomX, 8, 0), Quaternion.identity);
            yield return new WaitForSeconds(randomT);
        }


    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
 
    //IEnumerator EndOfWaveRoutine()
    //{
    //    _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    //    if (_uIManager != null)
    //    {
    //        _uIManager.WaveIncomingTextRoutine(_waveCount);
    //    }
    //    yield return new WaitForSeconds(4);
    //}
}
