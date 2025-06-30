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
            Vector3 movementVector = transform.TransformDirection(new Vector3(0, 0, _move.y));
            _rigidbody.linearVelocity = movementVector * _moveSpeed ;
            transform.Rotate(0,_move.x,0);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            _move = context.ReadValue<Vector2>();
        }
        
        #endregion
        
        
        #region Utils


        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private float _moveSpeed = 5f;

        private Vector2 _move;
        private Rigidbody _rigidbody;
        
        #endregion
    }
}
