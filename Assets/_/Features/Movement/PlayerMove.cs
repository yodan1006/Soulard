using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement.Runtime
{
    public class PlayerMove : MonoBehaviour
    {
        #region Api Unity

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {
        
        }

        
        void FixedUpdate()
        {
            Vector3 movementVector = transform.TransformDirection(new Vector3(_move.x, 0, _move.y));
            _rigidbody.linearVelocity = movementVector * _moveSpeed ;
            float rotate = _rotation.x * _rotationSpeed * _mouseSensitivity * Time.deltaTime;
            transform.Rotate(0,rotate,0);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }

        public void LookAt(InputAction.CallbackContext context)
        {
            _rotation = context.ReadValue<Vector2>();
        }
        
        #endregion
        
        
        #region Utils


        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _rotationSpeed = 90f;
        [SerializeField] private float _mouseSensitivity = 1.0f;

        private Vector2 _move;
        private Vector2 _rotation;
        private Rigidbody _rigidbody;
        
        #endregion
    }
}
