using System.Collections.Generic;
using UnityEditor.Animations;
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
                    armature.GetComponent<SkinnedMeshRenderer>().material = _colorSpawn;
                    AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
                    _animator.SetBool("Spawn", true);
                    if (stateInfo.IsName("Spawn") && stateInfo.normalizedTime >= 0.6f)
                    {
                        _animator.SetBool("Spawn", false);
                        _etat = Etat.Attack;
                    }
                    break;
                case Etat.idle:
                    if (_typeIa == TypeIA.civil)
                    {
                        armature.GetComponent<SkinnedMeshRenderer>().material = _colorPNJIdle;
                    }
                    break;
                case Etat.Attack:
                    armature.GetComponent<SkinnedMeshRenderer>().material = _colorAttack;
                    _agent.SetDestination(_target.transform.position);
                    transform.LookAt(_target.transform);
                    _timeAttack += Time.deltaTime;
                    _animator.SetBool("OnMove", true);
                    Attack(_target);
                    break;
                case Etat.vaumito:
                    armature.GetComponent<SkinnedMeshRenderer>().material = _colorVaumito;
                    break;
            }
        }

        private void Attack(GameObject target)
        {
            if (Time.timeScale == 0) return;
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
            if (Time.timeScale == 0) return;
            GameObject bottle = Instantiate(_bottlePrefab, _shootPoint.position, Quaternion.identity);
            Rigidbody rb = bottle.GetComponent<Rigidbody>();
            
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Bottle bottleScript = bottle.GetComponent<Bottle>();
            bottleScript.SetLauncher(gameObject);
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
            if (Time.timeScale == 0) return;
            if (_target != null)
            {
                JetBottle(_target);
            }
        }

         public void SetTarget(GameObject newTarget)
         {
             if (Time.timeScale == 0) return;
            if (_typeIa == TypeIA.enemy || _etat == Etat.Attack) return;
             _target = newTarget;
            _etat = Etat.Attack;
             _OnTouched = true;
         }

        #endregion


        #region Main Methode

        

        #endregion
        
        
        #region Privates
        
        [SerializeField] public NavMeshAgent _agent;
        public Etat _etat; 
        [SerializeField] private TypeIA _typeIa;
        private bool _OnAttack;
        private GameObject _target;
        [SerializeField] private GameObject _bottlePrefab;
        [SerializeField] private float _jetForce;
        [SerializeField] private float _timeAttack;
        [SerializeField] private float _interval;
        [SerializeField] private bool _OnTouched;
        [SerializeField] private List<AnimatorController> _animatorControllers;
        [SerializeField] public Animator _animator;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _tumbleForce = 10f;

        [SerializeField] private GameObject armature;
        [SerializeField] private Material _colorPNJIdle;
        [SerializeField] private Material _colorAttack;
        [SerializeField] private Material _colorSpawn;
        [SerializeField] private Material _colorVaumito;


        public enum Etat
        {
            idle,
            Attack,
            spawn,
            vaumito
        }
        #endregion

        private enum TypeIA
        {
            civil,
            enemy
        }
    }
}