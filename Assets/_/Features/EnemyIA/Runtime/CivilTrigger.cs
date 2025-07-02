using Movement.Runtime;
using UnityEngine;

namespace EnemyIa.Runtime
{
    public class CivilTrigger : MonoBehaviour
    {
        #region Api Unity
        
        void Start()
        {
            scriptEnemy = GetComponent<IAEnemy>();
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
                    scriptEnemy.SetTarget(bottle.m_launcher);
                    //Debug.Log(bottle.m_launcher.name);
                    other.gameObject.SetActive(false);
                    return;
                }
            }

            if (other.gameObject.layer == LayerMask.NameToLayer("BulletPlayer"))
            {
                scriptEnemy.SetTarget(_playerMove.gameObject);
                other.gameObject.SetActive(false);
            }
            

        }
        #endregion
        
        
        #region Utils

        private void Death()
        {
            if (_heath <= 0)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                scriptEnemy._animator.SetBool("Vaumito",true);
                _timeDispawn -= Time.deltaTime;
                scriptEnemy._agent.isStopped = true;
                scriptEnemy._etat = IAEnemy.Etat.vaumito;
                if (_timeDispawn <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        
        #endregion
        
        
        #region Private And Protected

        [SerializeField] private int _heath = 5;

        private IAEnemy scriptEnemy;
        private PlayerMove _playerMove;
        // [SerializeField] private  IAEnemy scriptEnemy;
        [SerializeField] private float _timeDispawn;

        #endregion
    }
}
