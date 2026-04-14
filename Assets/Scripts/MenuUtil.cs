using UnityEngine;
using UnityEngine.SceneManagement;

namespace Westhouse
{
    public class MenuUtil : MonoBehaviour
    {
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}