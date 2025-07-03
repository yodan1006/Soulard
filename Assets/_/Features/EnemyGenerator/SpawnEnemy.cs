using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyGenerator.Runtime
{
    public class SpawnEnemy : MonoBehaviour
    {
        #region Publics

        [SerializeField] public float m_interval;
        public int m_countSpawn;

        #endregion


        #region Unity Api

        private void Start()
        {
            
        }

        private void Update()
        {
            _timeForSpawnEnemy -= Time.deltaTime;

            if (_timeForSpawnEnemy <= 0)
            {
                SpawnEnemys();
                _timeForSpawnEnemy = m_interval;
            }
        }

        #endregion


        #region Utils

        

        #endregion


        #region Main Methode

        private void SpawnEnemys()
        {
            if (Time.timeScale == 0) return;
            for (int i = 0; i < _nbSpawn; i++)
            {
                
                GameObject enemy = poolEnemy.GetEnemy();
    
                if (enemy != null)
                {
                    Vector3 pos = GetRandomPos();
                    enemy.transform.position = pos;
                    m_countSpawn++;
                }
            }
        }

        private Vector3 GetRandomPos()
        {
            if (_spawnPoint.Count > 0)
            {
                Transform spawnPoint = _spawnPoint[Random.Range(0, _spawnPoint.Count)];
                return spawnPoint.position;
            }
            else return Vector3.zero;
        }

        #endregion
        
        
        #region Privates
        
        [SerializeField] private int _nbSpawn;
        [SerializeField] private PoolEnemy poolEnemy;
        [SerializeField] private List<Transform>_spawnPoint;
        [SerializeField] private float _timeForSpawnEnemy;

        #endregion
    }
}