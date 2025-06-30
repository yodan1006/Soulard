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
            if (other.gameObject.layer == _layerMask.value)
            {
                Damage();
            }

            if (other.gameObject.layer == _layerHealth.value)
            {
                if (_currentHealth < _maxHealth)
                {
                   Health();
                }
            }
        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            if (context.phase == InputActionPhase.Performed && _currentHealth == _maxHealth)
            {
                Instantiate(_colliderUltimate, transform.position, Quaternion.identity);
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
        
        [Header("Layer Damage")]
        [SerializeField] private LayerMask _layerMask;
        
        [Header("Layer Health")]
        [SerializeField] private LayerMask _layerHealth;
        
        [Header("Ultimate")]
        [SerializeField] private Collider _colliderUltimate;
        
        private int _currentHealth;
        private bool _ultimate;

        #endregion
    }
}
