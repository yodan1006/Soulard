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
           
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Start()
        {
        
        }

        
        void FixedUpdate()
        {
            Vector3 movementVector = transform.TransformDirection(new Vector3(_move.x, 0, _move.y));
            _rigidbody.linearVelocity = movementVector * _moveSpeed ;
            Vector2 rotate = new Vector2(_rotation.x * _mouseSensitivity * Time.deltaTime, _rotation.y * _mouseSensitivity * Time.deltaTime);
            transform.Rotate(Vector3.up * rotate.x);
            
            _xRotation -= rotate.y;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            Quaternion rotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _camera.localRotation = rotation;
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (Time.timeScale == 0) return;
            _move = context.ReadValue<Vector2>();
        }

        public void LookAt(InputAction.CallbackContext context)
        {
            if (Time.timeScale == 0) return;
            _rotation = context.ReadValue<Vector2>();
        }
        
        #endregion
        
        
        #region Utils


        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _mouseSensitivity = 1.0f;
        [SerializeField] private Transform _camera;

        private Vector2 _move;
        private Vector2 _rotation;
        private Rigidbody _rigidbody;
        private float _xRotation;
        
        #endregion
    }
}
