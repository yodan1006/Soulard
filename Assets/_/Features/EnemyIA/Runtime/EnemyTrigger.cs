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
            scriptEnemy = GetComponent<IAEnemy>();
            _poolEnemy = FindFirstObjectByType<PoolEnemy>();
            _timeOrigin = _timeDispawn;
        }

        private void Update()
        {
            if (_OnAnimeTouchPlay)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                scriptEnemy._animator.SetBool("Vaumito",true);
                _timeDispawn -= Time.deltaTime;
                scriptEnemy._agent.isStopped = true;
                scriptEnemy._etat = IAEnemy.Etat.vaumito;
                if (_timeDispawn <= 0)
                {
                    gameObject.SetActive(false);
                    _poolEnemy.ReturnObject(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (Time.timeScale == 0) return;
            if (other.gameObject.layer == LayerMask.NameToLayer("BulletPlayer") || other.gameObject.layer == LayerMask.NameToLayer("BulletCivil"))
            {
                //animator.SetBool()
                _OnAnimeTouchPlay = true;
                _heath--;
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
        [SerializeField] private int _heath = 5;
        private IAEnemy scriptEnemy;

        #endregion
    }
}
