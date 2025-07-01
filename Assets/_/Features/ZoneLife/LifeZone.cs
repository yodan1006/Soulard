using UnityEngine;
using UnityEngine.Serialization;

namespace Life.Runtime
{
    public class LifeZone : MonoBehaviour
    {
        #region Publics
        
        [HideInInspector]
        public int m_currentnumbers;
        [HideInInspector]
        public int m_life;
        
        #endregion
        
        
        #region Api Unity
        void Start()
        {
            m_currentnumbers = _bottle;
            m_life = _life;
        }

        
        void Update()
        {
            if (m_currentnumbers < _bottle)
            {
                _timeReloadTimer += Time.deltaTime;
                Reload();
            }
            
        }
        
        #endregion
        
        
        #region Private And Protected

        private void Reload()
        {
            if (_timeReloadTimer >= _timeReload)
            {
                _timeReloadTimer = 0;
                m_currentnumbers += _numberReload;
                Debug.Log("Reload");
            }
        }

        #endregion
        
        
        #region Private And Protected

        [Header("Number of Bottles Max")]
        [SerializeField] private int _bottle = 3;
        
        [Header("Time Reload Bottle")]
        [SerializeField] private float _timeReload = 5;
        private float _timeReloadTimer;
        [Header("Number of Bottles Reload")]
        [SerializeField]private int _numberReload = 2;

        [Header("Life Health")] [SerializeField]
        private int _life = 1;

        #endregion
    }
}
