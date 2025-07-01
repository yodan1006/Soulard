using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyIa.Runtime
{
    public class IAEnemy : MonoBehaviour
    {
        #region Publics

        

        #endregion


        #region Unity Api

        private void Start()
        {
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
                if (playerObject != null)
                    _target = playerObject;
            }

        }

        private void Update()
        {
            switch (_etat)
            {
                case Etat.idle:
                    if (_typeIa == TypeIA.civil)
                    {
                        //_animator.SetBool()
                    }
                    break;
                case Etat.Attack:
                    _timeAttack += Time.deltaTime;
                    if (_timeAttack >= _interval)
                    {
                        Attack(_target);
                        _timeAttack = 0.0f;
                    }
                    break;
            }
        }

        private void Attack(GameObject target)
        {
            _agent.SetDestination(target.transform.position);
            if (Vector3.Distance(_agent.transform.position, target.transform.position) < _distanceForMelee)
                Melee();
            else
                JetBottle(target);
        }

        private void JetBottle(GameObject target)
        {
            GameObject bottle = Instantiate(_bottlePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bottle.GetComponent<Rigidbody>();

            Bottle bottleScript = bottle.GetComponent<Bottle>();
            bottleScript.launcher = gameObject;
            if (rb != null)
            {
                Vector3 direction = (target.transform.position - transform.position).normalized;
                rb.AddForce(direction * _jetForce, ForceMode.Impulse);
            }
        }

        private void Melee()
        {
            Debug.Log("Melee");
        }

        #endregion


        #region Utils

        public void SetTarget(GameObject newTarget)
        {
            if (_typeIa == TypeIA.enemy || _etat == Etat.Attack) return;
            _target = newTarget;
            _etat = Etat.Attack;
            _OnTouched = true;
        }

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates
        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Etat _etat;
        [SerializeField] private TypeIA _typeIa;
        private bool _OnAttack;
        private GameObject _target;
        [SerializeField] private float _distanceForMelee;
        [SerializeField] private GameObject _bottlePrefab;
        [SerializeField] private float _jetForce;
        [SerializeField] private float _timeAttack;
        [SerializeField] private float _interval;
        [SerializeField] private bool _OnTouched;


        private enum Etat
        {
            idle,
            Attack,
        }
        #endregion

        private enum TypeIA
        {
            civil,
            enemy
        }
    }
}
