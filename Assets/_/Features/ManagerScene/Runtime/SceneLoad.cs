using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagerScene.Runtime
{
    public class SceneLoad : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
