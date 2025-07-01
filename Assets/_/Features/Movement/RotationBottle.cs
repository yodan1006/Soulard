using UnityEngine;

namespace Movement.Runtime
{
    public class RotationBottle : MonoBehaviour
    {
        #region Api Unity

        void Update()
        {
            Rotate();
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
            _rotation.z -= 90f;
            transform.Rotate(_rotation, 720f * Time.deltaTime );
        }
        
        #endregion
        
        
        #region Private And Protected
        
        private Vector3 _rotation;
        
        #endregion
    }
}
