using UnityEngine;
using UnityEngine.InputSystem;

namespace MenuPause.Runtime
{
    public class PauseMenu : MonoBehaviour
    {
        public void Pause(InputAction.CallbackContext context)
        {
            if (context.started)
                Time.timeScale = 0;
        }

        public void Resume(InputAction.CallbackContext context)
        {
            if (context.started && Time.timeScale == 0)
                Time.timeScale = 1;
        }
    }
}
