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
                case Etat.spawn:
                    
                    break;
                case Etat.idle:
                    if (_typeIa == TypeIA.civil)
                    {
                        //
                    }
                    break;
                case Etat.Attack:
                    _agent.SetDestination(_target.transform.position);
                    transform.LookAt(_target.transform);
                    _timeAttack += Time.deltaTime;
                    _animator.SetBool("OnMove", true);
                    Attack(_target);
                    break;
            }
        }

        private void Attack(GameObject target)
        {
            // On vérifie s'il est temps de lancer une nouvelle attaque
            if (_timeAttack >= _interval)
            {
                // On déclenche l'animation d'attaque.
                // L'événement d'animation s'occupera de lancer la bouteille au bon moment.
                _animator.SetBool("OnAttack", true);
                _timeAttack = 0.0f; // On réinitialise le compteur
            }

            // On garde cette logique pour réinitialiser le booléen une fois l'animation terminée.
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("Throw") && stateInfo.normalizedTime >= 0.6f)
            {
                _animator.SetBool("OnAttack", false);
            }
        }

        private void JetBottle(GameObject target)
        {
            GameObject bottle = Instantiate(_bottlePrefab, _shootPoint.position, Quaternion.identity);
            Rigidbody rb = bottle.GetComponent<Rigidbody>();
            
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Bottle bottleScript = bottle.GetComponent<Bottle>();
            bottleScript.InitializeTumble(dir, _tumbleForce);

            //bottleScript.launcher = gameObject;
            //bottleScript.InitDirection(dir);
            if (rb != null)
            {
                rb.AddForce(dir * _jetForce, ForceMode.Impulse);
            }
        }

        #endregion


        #region Utils

        // AJOUTEZ CETTE NOUVELLE MÉTHODE PUBLIQUE
        // Elle sera appelée par l'événement d'animation.
        public void AnimationEvent_ThrowBottle()
        {
            if (_target != null)
            {
                JetBottle(_target);
            }
        }

        // public void SetTarget(GameObject newTarget)
        // {
        //     if (_typeIa == TypeIA.enemy || _etat == Etat.Attack) return;
        //     _target = newTarget;
        //     _etat = Etat.Attack;
        //     _OnTouched = true;
        // }

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
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _tumbleForce = 10f; 



        private enum Etat
        {
            idle,
            Attack,
            spawn
        }
        #endregion

        private enum TypeIA
        {
            civil,
            enemy
        }
    }
}