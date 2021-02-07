using UnityEngine;

public class MenuController : MonoBehaviour
{
    bool _paused = false;

    public delegate void MenuEvent();
    public static event MenuEvent OnPause;
    public static event MenuEvent OnResume;

    void Update()
    {
        CheckPauseOrResume();
    }

    void CheckPauseOrResume()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!_paused)
                Pause();
            else
                Resume();
        }
    }

    void Pause()
    {
        _paused = true;
        Time.timeScale = 0f;
        OnPause?.Invoke();
    }

    void Resume()
    {
        _paused = false;
        Time.timeScale = 1f;
        OnResume?.Invoke();
    }
}