using System;
using EnemyGenerator.Runtime;
using UnityEngine;

namespace EnemyIa.Runtime
{
    public class EnemyTrigger : MonoBehaviour
    {
        

        #region Publics

        

        #endregion


        #region Unity Api

        private void Start()
        {
            _poolEnemy = FindFirstObjectByType<PoolEnemy>();
            _timeOrigin = _timeDispawn;
        }

        private void Update()
        {
            if (_OnAnimeTouchPlay)
            {
                _timeDispawn -= Time.deltaTime;
                if (_timeDispawn >= 0)
                {
                    gameObject.SetActive(false);
                    _poolEnemy.ReturnObject(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BulletPlayer"))
            {
                //animator.SetBool()
                _OnAnimeTouchPlay = true;
                Debug.Log("il m'a toucher");
            }
        }

        #endregion


        #region Utils

        

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates
        
        private float _timeOrigin;
        [SerializeField] private float _timeDispawn;
        private bool _OnAnimeTouchPlay;
        [SerializeField]private PoolEnemy _poolEnemy;

        #endregion
    }
}
