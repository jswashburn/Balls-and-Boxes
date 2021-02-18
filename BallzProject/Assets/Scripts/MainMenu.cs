using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Range(0, 4)][SerializeField] byte _depth;

    public void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
        LaunchInputManager.Controlable = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
