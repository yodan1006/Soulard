using System;
using System.Collections.Generic;
using UnityEditor.Animations;
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

        private void Awake()
        {
        }

        private void Start()
        {
            int random = UnityEngine.Random.Range(0, _animatorControllers.Count);
            _animator.runtimeAnimatorController = _animatorControllers[random];
            
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
                    _animator.SetBool("OnMove", true);
                    Attack(_target);
                    break;
            }
        }

        private void Attack(GameObject target)
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            _agent.SetDestination(target.transform.position);
            if (Vector3.Distance(_agent.transform.position, target.transform.position) < _distanceForMelee)
                Melee();
            else if (_timeAttack >= _interval)
            {
                _animator.SetBool("OnAttack", true);
                JetBottle(target);
                _timeAttack = 0.0f;
                if (stateInfo.IsName("Throw") && stateInfo.normalizedTime >= 1)
                    _animator.SetBool("OnAttack", false);
            }

        }

        private void JetBottle(GameObject target)
        {
            GameObject bottle = Instantiate(_bottlePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bottle.GetComponent<Rigidbody>();
            
            Vector3 dir = (target.transform.position - transform.position).normalized;

            Bottle bottleScript = bottle.GetComponent<Bottle>();
            bottleScript.launcher = gameObject;
            bottleScript.InitDirection(dir);
            if (rb != null)
            {
                rb.AddForce(dir * _jetForce, ForceMode.Impulse);
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
        [SerializeField] private List<AnimatorController> _animatorControllers;
        [SerializeField] private Animator _animator;


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
