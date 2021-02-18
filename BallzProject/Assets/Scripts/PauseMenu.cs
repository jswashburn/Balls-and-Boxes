using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour, IColorChangeable
{
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] TextMeshProUGUI _pauseButton;
    [SerializeField] TextMeshProUGUI _resumeButton;
    [SerializeField] TextMeshProUGUI _mainMenuButton;
    [SerializeField] TextMeshProUGUI _quitButton;
    [Range(0, 4)][SerializeField] byte _depth;

    public static bool Paused { get; private set; } = false;
    public byte Depth { get; private set; }

    void Awake()
    {
        Depth = _depth;
    }

    void Update()
    {
        CheckPauseOrResume();
    }

    void CheckPauseOrResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!Paused)
            {
                Pause();
                ActivatePauseMenu();
            }
            else
            {
                Resume();
                DeactivatePauseMenu();
            }
        }
    }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Paused = false; 
        Time.timeScale = 1f;
    }

    public void ActivatePauseMenu() => _pauseMenu.SetActive(true);

    public void DeactivatePauseMenu() => _pauseMenu.SetActive(false);

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame() => Application.Quit(0);

    public void ChangeColor(Color32 color)
    {
        _resumeButton.color = color;
        _mainMenuButton.color = color;
        _quitButton.color = color;
        _pauseButton.color = color;
    }
}
