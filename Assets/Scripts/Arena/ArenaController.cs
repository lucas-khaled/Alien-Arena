using System;
using System.Collections;
using System.Collections.Generic;
using AlienArena.Enemies;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace AlienArena.Arena
{
    public class ArenaController : MonoBehaviour
    {
        [SerializeField] private GameObject recharger;
        [SerializeField] private ArenaSettings settings;
        [SerializeField] private Transform[] spawnPoints;
        [SerializeField] private float startDelay = 2;
        
        private List<Enemy> _enemiesList = new List<Enemy>();
        private Player.Player _player;
        
        private Challenge _challenge;
        private ArenaEnemyData[] _enemyData;
        
        private int _quantityOfEnemies;
        private int _quantityOfSpawns;

        
        private void Awake()
        {
            _challenge = settings.actualChallenge;
            _quantityOfEnemies = _challenge.GetEnemiesTotalCount();
            _enemyData = (ArenaEnemyData[])_challenge.enemies.Clone();
        }

        private void Start()
        {
            StartCoroutine(SpawnEnemiesRoutine());
            _player = Player.Player.instance;
            _player.onDeath += PlayerDied;

            SpawnRechargers();
        }

        private void SpawnRechargers()
        {
            foreach (var spawn in spawnPoints)
            {
                Instantiate(recharger, spawn.position, quaternion.identity);
            }
            
        }

        private void PlayerDied()
        {
            SceneManager.LoadScene("Level");
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            int totalEnemiesCount = _challenge.GetEnemiesTotalCount();
            yield return new WaitForSeconds(startDelay);

            while (_quantityOfSpawns < totalEnemiesCount)
            {
                Enemy enemy = GetRandomEnemyInDataList();

                Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
                Enemy instantiatedEnemy = Instantiate(enemy, spawnPosition, quaternion.identity);

                instantiatedEnemy.onEnemyDeath += EnemyDeath;
                
                _enemiesList.Add(instantiatedEnemy);

                float waitSeconds = Random.Range(_challenge.maxInstanceTime, _challenge.maxInstanceTime);
                _quantityOfSpawns++;
                yield return new WaitForSeconds(waitSeconds);
            }
        }

        private void EnemyDeath(Enemy enemy)
        {
            enemy.onEnemyDeath -= EnemyDeath;
            _enemiesList.Remove(enemy);
            _quantityOfEnemies--;

            if (_quantityOfEnemies <= 0)
            {
                _player.AddCoins(_challenge.coinsReward);
                _challenge.completed = true;
                SceneManager.LoadScene("Level");
            }
        }

        private Enemy GetRandomEnemyInDataList(int index = -1, int iteractions = 0)
        {
            int realIndex = (index <= -1) ? Random.Range(0, _enemyData.Length) : index;
            //Debug.Log("Real: "+realIndex+" --- Index: "+index+" --- Iteractions: "+iteractions);

            if (iteractions > _enemyData.Length)  return null;

            if (_enemyData[realIndex].quantity <= 0)
                return GetRandomEnemyInDataList(++index, ++iteractions);
            
            _enemyData[realIndex].quantity--;
            return _enemyData[realIndex].enemy;
            
        }
    }
}