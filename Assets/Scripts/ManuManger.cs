using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuManger : MonoBehaviour
{
   
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
