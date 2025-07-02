using Movement.Runtime;
using UnityEngine;

namespace ZoneSpawn.Runtime
{
    public class ActiveTrigger : MonoBehaviour
    {
        private void Start()
        {
            _vieDuPlayer = FindFirstObjectByType<PlayerStat>();
        }

        private void Update()
        {
            if (_vieDuPlayer.m_currentHealth >= _vieActiveSecondSpawn)
            {
                ActiveSecondSpawn();
            }
        }

        private void ActiveSecondSpawn()
        {
            _spawneurSecondaire.SetActive(true);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
               _spawneurPrincipal.SetActive(true); 
            }
        }

        [SerializeField] private int _vieActiveSecondSpawn;
        [SerializeField] private GameObject _spawneurPrincipal;
        [SerializeField] private GameObject _spawneurSecondaire;
        private PlayerStat _vieDuPlayer;
    }
}
