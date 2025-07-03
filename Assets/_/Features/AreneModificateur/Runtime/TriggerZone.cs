using System;
using EnemyGenerator.Runtime;
using UnityEngine;

namespace AreneModificateur.Runtime
{
    public class TriggerZone : MonoBehaviour
    {
        [SerializeField] private SpawnEnemy _spawnEnemy;
        [SerializeField] private float _modificateur;

        private void OnTriggerEnter(Collider other)
        {
            if (Time.timeScale == 0) return;
            _spawnEnemy.m_interval -= _modificateur;
        }
    }
}
