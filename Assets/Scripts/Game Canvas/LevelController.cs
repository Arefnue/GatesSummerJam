using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Canvas
{
    public class LevelController : MonoBehaviour
    {
        private const int MenuSceneNumber = 0;


        public void GoToMenu()
        {
            SceneManager.LoadScene(MenuSceneNumber);

            ResetTimeScale();
        }


        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        

        public void GoToNextScene()
        {
            int maxScene = SceneManager.sceneCountInBuildSettings;
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            if(currentScene + 1 < maxScene)
            {
                SceneManager.LoadScene(currentScene + 1); // Go to next scene
            }
            // No next scene
            else
            {
                SceneManager.LoadScene(MenuSceneNumber); // go to menu
            }

            ResetTimeScale();
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartScene()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentScene);

            ResetTimeScale();
        }

        private void ResetTimeScale()
        {
            Time.timeScale = 1;
        }
    }
}
