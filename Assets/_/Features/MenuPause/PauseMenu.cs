using UnityEngine;
using UnityEngine.InputSystem;

namespace MenuPause.Runtime
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUI;
        private bool _isPaused;

        public void Pause(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            _isPaused = !_isPaused;
            
            if (_isPaused)
            {
                Time.timeScale = 0;
                pauseUI.SetActive(true);
            }
            else
            {
                Resume();
            }
        }

        public void Resume()
        {
            _isPaused = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }
}