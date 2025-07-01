using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Movement.Runtime
{
    public class PlayerStat : MonoBehaviour
    {
        #region Publics
        
        private int m_currentHealth;
        
        #endregion
        
        #region Api Unity

        private void Awake()
        {
            _currentHealth = _maxHealth/2;
            _healthSlider.value = _currentHealth;
            _healthSlider.maxValue = _maxHealth;
        }

        void Start()
        {
            
        }

        
        void Update()
        {
            _healthSlider.value = _currentHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BulletEnemy"))
            {
                Damage();
            }


        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Reload"))
            {   
                _timerHealth -= Time.deltaTime;
                if (_currentHealth < _maxHealth && _timerHealth >= _timerHealth)
                {
                    Health();
                }
            }
        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            if (context.performed && _currentHealth == _maxHealth)
            {
                _colliderUltimate.enabled = true;
                _ultimateTimer += Time.deltaTime;
                if (_ultimateTimer >= 2) _colliderUltimate.enabled = false;
            }
            
        }

        #endregion
        
        
        #region Main Methods
        
        
        
        #endregion
        
        
        #region Utils

        [ContextMenu("Damage")]
        private void Damage()
        {
            _currentHealth--;
            m_currentHealth = _currentHealth;
            Death();
        }

        [ContextMenu("Health")]
        private void Health()
        {
            if (_currentHealth < _maxHealth)
            {
                _currentHealth++;
                m_currentHealth = _currentHealth;
            }
        }
        

        private void Death()
        {
            if (_currentHealth <= 0) gameObject.SetActive(false);
        }
        
        
        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private int _maxHealth = 10;
        [SerializeField] private Slider _healthSlider;
        
        [Header("Time Health")]
        [SerializeField] private float _timerHealth = 3f;
        
        [Header("Layer Damage")]
        [SerializeField] private LayerMask _layerMask;
        
        [Header("Layer Health")]
        [SerializeField] private LayerMask _layerHealth;
        
        [Header("Ultimate")]
        [SerializeField] private Collider _colliderUltimate;
        
        private int _currentHealth;
        private bool _ultimate;
        [SerializeField] private float _ultimateTimer;
        

        #endregion
    }
}
