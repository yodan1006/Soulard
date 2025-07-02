using Movement.Runtime;
using UnityEngine;

namespace EnemyIa.Runtime
{
    public class CivilTrigger : MonoBehaviour
    {
        #region Api Unity
        
        void Start()
        {
            _iaEnemy = GetComponent<IAEnemy>();
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                _playerMove = playerObj.GetComponent<PlayerMove>();
        }

        void Update()
        {
            Death();
        }

        void OnTriggerEnter(Collider other)
        {
            _heath--;
            if (other.gameObject.layer == LayerMask.NameToLayer("BulletEnemy"))
            {
                Bottle bottle = other.GetComponent<Bottle>();
                if (bottle != null && bottle.m_launcher != null)
                {
                    _iaEnemy.SetTarget(bottle.m_launcher);
                    //Debug.Log(bottle.m_launcher.name);
                    other.gameObject.SetActive(false);
                    return;
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("BulletPlayer"))
            {
                _iaEnemy.SetTarget(_playerMove.gameObject);
                other.gameObject.SetActive(false);
            }
            

        }
        #endregion
        
        
        #region Utils

        private void Death()
        {
            if (_heath <= 0) gameObject.SetActive(false);
        }
        
        #endregion
        
        
        #region Private And Protected

        [SerializeField] private int _heath = 5;

        private IAEnemy _iaEnemy;
        private PlayerMove _playerMove;

        
        #endregion
    }
}
