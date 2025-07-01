using System;
using UnityEngine;

namespace Environment.Runtime
{
    public class DoorLife : MonoBehaviour
    {
        #region Api Unity

        private void Awake()
        {
            _meshIdle = GetComponent<MeshFilter>();
        }

        private void Update()
        {
            Death();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("BulletPlayer"))
            {
                _doorLife--;
                other.gameObject.SetActive(false);
            }
        }

        #endregion
        
        
        #region Utils

        private void Death()
        {
            if (_doorLife <= 0)
            {
                _collider.enabled = false;
                _meshIdle.mesh = _meshFilter;
            }
        }
        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private int _doorLife = 10;
        [SerializeField] private Collider _collider;
        [SerializeField] private Mesh _meshFilter;

        private MeshFilter _meshIdle;

        #endregion

    }
}
