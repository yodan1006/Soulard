using System;
using UnityEngine;

namespace Movement.Runtime
{
    public class RotationBottle : MonoBehaviour
    {
        #region Api Unity

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void OnEnable()
        {
            _currentTimeLife = _timeLife;
            transform.right = _player.transform.right;
        }

        void Update()
        {
            Rotate();
            _currentTimeLife -= Time.deltaTime;
            if (_currentTimeLife < 0) gameObject.SetActive(false);
        }
        
        #endregion
        
        
        #region Main Methods

        public void InitDirection(Vector3 direction)
        {
            _rotation = direction.normalized;
        }
        
        #endregion
        
        
        #region Utils

        private void Rotate()
        {
            _rotation.x += 90f;
            transform.Rotate(_rotation, 720f * Time.deltaTime );
        }
        
        #endregion
        
        
        #region Private And Protected
        
        private Vector3 _rotation;

        [Header("Time Ammo Life")]
        [SerializeField] private float _timeLife = 3f;
        private float _currentTimeLife;

        [SerializeField] private GameObject _player;

        #endregion
    }
}
