using UnityEngine;

namespace Environment.Runtime
{
    public class Gyrophare : MonoBehaviour
    {
        #region Api Unity
        void Start()
        {
        
        }

        
        void Update()
        {
            transform.Rotate(Vector3.up, Time.deltaTime * _speed);
        }
        #endregion
        
        
        #region Private And Protected

        [SerializeField] private float _speed = 10f;
        

        #endregion
    }
}
