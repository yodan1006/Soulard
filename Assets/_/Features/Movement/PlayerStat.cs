using System;
using Life.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Movement.Runtime
{
    public class PlayerStat : MonoBehaviour
    {
        #region Publics
        
        [HideInInspector]
        public int m_currentHealth;
        
        #endregion
        
        #region Api Unity

        private void Awake()
        {
            _currentHealth = _maxHealth/2;
            _healthSlider.value = _currentHealth;
            _healthSlider.maxValue = _maxHealth;
            _zone = FindObjectsByType<LifeZone>(FindObjectsSortMode.None)[0];
            
        }
        
        void Update()
        {
            _healthSlider.value = _currentHealth;
            m_currentHealth = _currentHealth;
            
            if (_ultimate)
            {
                _delayTimeUltimate += Time.deltaTime;
                _currentHealth = _maxHealth/2;
            }
            if (_delayTimeUltimate >= _ultimateTimer)
            {
                _colliderUltimate.enabled = false;
                _ultimate = false;
            }
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
            var zone = other.GetComponent<LifeZone>();
            if (zone != null)
            {   
                _zone = zone;
                _zoneLife = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var zone = other.GetComponent<LifeZone>();
            if (zone != null && zone == _zone)
            {
                _zone = null;
                _zoneLife = false;
            }
        }

        public void Ultimate(InputAction.CallbackContext context)
        {
            if (context.performed && _currentHealth == _maxHealth)
            {
                _colliderUltimate.enabled = true;
                _ultimate = true;
            }
            
        }

        public void PickUpBottle(InputAction.CallbackContext context)
        {
            if (context.started && _zone != null)
            {
                if (_zone.m_currentnumbers >= 1)
                {
                    _zone.m_currentnumbers -= 1;
                    Health();
                    Debug.Log("Picked up bottle");
                }
                
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
            Death();
        }

        [ContextMenu("Health")]
        private void Health()
        {
            if (_currentHealth < _maxHealth && _zone.m_currentnumbers > 0)
            {
                _currentHealth+= _zone.m_life;
            }
            if(_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
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
        
        private bool _zoneLife;
        private LifeZone _zone;
        
        [Header("Ultimate")]
        [SerializeField] private Collider _colliderUltimate;
        [SerializeField] private float _ultimateTimer;

        
        private int _currentHealth;
        private bool _ultimate;
        private float _delayTimeUltimate;
        
        

        #endregion
    }
}
