using UnityEngine;
using UnityEngine.InputSystem;

namespace ManagerScene.Runtime
{
    public class PauseMenu : MonoBehaviour
    {
        #region Api Unity
        void Start()
        {
        
        }

        
        void Update()
        {   
            
        }
        #endregion
        
        
        #region Utils

        public void Pause()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0;
            _isPaused= true;
            
            _playerInput.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }

        public void Resume()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1;
            _isPaused= false;
            _playerInput.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void Exit()
        {
            Application.Quit();
        }
        
        #endregion
        
        
        #region Private And Protected
        
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private GameObject _pauseMenu;
        private bool _isPaused = false;
        
        #endregion
    }
}
