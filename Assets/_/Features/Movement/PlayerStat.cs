using System;
using UnityEngine;
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
            _currentHealth = _maxHealth;
        }

        void Start()
        {
            
        }

        
        void Update()
        {
            
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == _layerMask.value)
            {
                _currentHealth--;
                m_currentHealth = _currentHealth;
                Death();
            }

            if (other.gameObject.layer == _layerHealth.value)
            {
                _currentHealth++;
                m_currentHealth = _currentHealth;
            }
        }

        #endregion
        
        
        #region Main Methods
        
        
        
        #endregion
        
        
        #region Utils
        
        

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
        
        private int _currentHealth;

        #endregion
    }
}
