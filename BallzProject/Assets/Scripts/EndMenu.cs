using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour, IColorChangeable
{
    [SerializeField] GameObject _endMenu;
    [SerializeField] TextMeshProUGUI _restartButton;
    [SerializeField] TextMeshProUGUI _quitButton;
    [SerializeField] TextMeshProUGUI _mainMenuButton;
    [SerializeField] TextMeshProUGUI _timeSurvivedText;
    [SerializeField] GameStateManager _gameStateManager;
    [Range(0, 4)][SerializeField] byte _depth;

    public static bool Paused { get; private set; } = false;
    public byte Depth { get; private set; }

    public void ChangeColor(Color32 color)
    {
        _restartButton.color = color;
        _quitButton.color = color;
        _mainMenuButton.color = color;
        _timeSurvivedText.color = color;
    }

    void Awake()
    {
        Depth = _depth;
    }

    void OnEnable()
    {
        GameStateManager.OnHealthBelowZero += GameOver;
    }

    void OnDisable()
    {
        GameStateManager.OnHealthBelowZero -= GameOver;
    }

    void GameOver()
    {
        Pause();
        _timeSurvivedText.text = $"You survived {_gameStateManager.survivedTime:0.##} seconds!";
        _endMenu.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        LaunchInputManager.Controlable = true;
        GameStateManager.CurrentHealth = 5;
        Resume();
    } 

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        LaunchInputManager.Controlable = true;
        GameStateManager.CurrentHealth = 5;
        Resume();
    }

    public void Pause()
    {
        Paused = true;
        Time.timeScale = 0f;
        LaunchInputManager.Controlable = false;
    }

    public void Resume()
    {
        Paused = false; 
        Time.timeScale = 1f;
        LaunchInputManager.Controlable = true;
    }
}
