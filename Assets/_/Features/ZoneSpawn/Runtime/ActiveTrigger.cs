using EnemyGenerator.Runtime;
using Movement.Runtime;
using System.Collections;
using UnityEngine;

namespace ZoneSpawn.Runtime
{
    public class ActiveTrigger : MonoBehaviour
    {
        private void Start()
        {
            _vieDuPlayer = FindFirstObjectByType<PlayerStat>();
            _CountSpawn = _spawneurPrincipal.GetComponent<SpawnEnemy>();
        }

        private void Update()
        {
            // Vérifie si la condition d'ouverture est remplie et que la porte n'est pas déjà en train de s'ouvrir
            if (!_isDoorOpening && _CountSpawn.m_countSpawn >= _nbLimitSpawn)
            {
                _isDoorOpening = true;
                StartCoroutine(OpenDoorCoroutine());
            }
            if (_vieDuPlayer.m_currentHealth >= _vieActiveSecondSpawn)
            {
                ActiveSecondSpawn();
            }
        }
        
        /// <summary>
        /// Coroutine pour ouvrir la porte de manière fluide.
        /// </summary>
        private IEnumerator OpenDoorCoroutine()
        {
            float duration = 2.0f; // Durée de l'ouverture de la porte en secondes
            float elapsedTime = 0f;
            
            Vector3 startPosition = _porte.transform.position;
            // On suppose que _pointPorte2 est un GameObject vide marquant la position finale de la porte.
            Vector3 endPosition = _pointPorte2.transform.position;

            while (elapsedTime < duration)
            {
                // Interpole la position de la porte entre la position de départ et la position de fin
                _porte.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null; // Attend la prochaine frame
            }

            // S'assure que la porte est exactement à la position finale
            _porte.transform.position = endPosition;
            gameObject.SetActive(false); // Désactive le trigger
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

        [SerializeField] private int _nbLimitSpawn;
        [SerializeField] private int _vieActiveSecondSpawn;
        [SerializeField] private GameObject _spawneurPrincipal;
        [SerializeField] private GameObject _spawneurSecondaire;
        [SerializeField] private GameObject _porte;
        [SerializeField] private GameObject _pointPorte1;
        [SerializeField] private GameObject _pointPorte2;
        private PlayerStat _vieDuPlayer;
        private SpawnEnemy _CountSpawn;
        private bool _isDoorOpening = false; // Pour éviter de lancer l'animation plusieurs fois
    }
}